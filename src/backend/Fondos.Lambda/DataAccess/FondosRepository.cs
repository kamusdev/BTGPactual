using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Fondos.Lambda.DataAccess.Interfaces;
using Fondos.Lambda.Models;
using Fondos.Lambda.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondos.Lambda.DataAccess
{
    public class FondosRepository : IFondosRepository
    {
        private readonly string _tableName = "FondosBTG";
        private readonly IAmazonDynamoDB _amazonDynamoDBClient;

        public FondosRepository(IAmazonDynamoDB amazonDynamoDB)
        {
            _amazonDynamoDBClient = amazonDynamoDB;
        }

        public async Task<IClient> GetClientByIdAsync(string id)
        {
            var table = Table.LoadTable(_amazonDynamoDBClient, _tableName);

            var config = new GetItemOperationConfig
            {
                AttributesToGet = new List<string> { "pk", "sk", "name", "email", "balance" },
                ConsistentRead = true
            };

            var document = await table.GetItemAsync("C", id, config);

            return document != null ? TranslateToClientModel(document) : null;
        }

        public async Task<IEnumerable<IFund>> GetFundsAsync()
        {
            var queryRequest = new QueryRequest
            {
                TableName = _tableName,
                KeyConditionExpression = "pk = :v_pk",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":v_pk", new AttributeValue { S = "F" } }
                }
            };

            var response = await _amazonDynamoDBClient.QueryAsync(queryRequest);

            return response.Items.Count > 0 ? TranslateToFundModel(response.Items) : [];
        }

        public async Task PostOpeningAsync(IOpening opening)
        {
            // U Mandate
            var mandateId = Guid.NewGuid().ToString();

            var mandate = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = "M" } },
                { "sk", new AttributeValue { S = mandateId } },
                { "clientId", new AttributeValue { S = opening.ClientId.ToString() } },
                { "fundId", new AttributeValue { S = opening.FundId.ToString() } },
                { "date", new AttributeValue { S = opening.Date.ToString("yyyy-MM-ddTHH:mm:ssZ") } },
                { "value", new AttributeValue { N = opening.Value.ToString() } },
            };

            var mandateRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = mandate
            };

            _ = await _amazonDynamoDBClient.PutItemAsync(mandateRequest);

            // Create transaction
            var transaction = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = "T" } },
                { "sk", new AttributeValue { S = Guid.NewGuid().ToString() } },
                { "mandateId", new AttributeValue { S = mandateId } },
                { "clientId", new AttributeValue { S = opening.ClientId.ToString() } },
                { "fundId", new AttributeValue { S = opening.FundId.ToString() } },
                { "operationType", new AttributeValue { N = ((int)opening.OperationType).ToString() } },
                { "date", new AttributeValue { S = opening.Date.ToString("yyyy-MM-ddTHH:mm:ssZ") } },
                { "value", new AttributeValue { N = opening.Value.ToString() } },
            };

            var transactionRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = transaction
            };

            _ = await _amazonDynamoDBClient.PutItemAsync(transactionRequest);

            // Update client balance
            var client = await GetClientByIdAsync(opening.ClientId.ToString());
            client.Balance -= opening.Value;

            var clientRecord = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = "C" } },
                { "sk", new AttributeValue { S = client.Id.ToString() } },
                { "balance", new AttributeValue { N = client.Balance.ToString() } },
                { "email", new AttributeValue { S = client.Email } },
                { "name", new AttributeValue { S = client.Name } },
            };

            var clientRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = clientRecord
            };

            _ = await _amazonDynamoDBClient.PutItemAsync(clientRequest);
        }

        public async Task PostClosureAsync(IClosure closure)
        {
            // Update Mandate
            var mandate = await GetMandateByIdAsync(closure.MandateId);
            mandate.Value = 0;

            var mandateToUpdate = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = "M" } },
                { "sk", new AttributeValue { S = mandate.Id.ToString() } },
                { "clientId", new AttributeValue { S = mandate.ClientId.ToString() } },
                { "fundId", new AttributeValue { S = mandate.FundId.ToString() } },
                { "date", new AttributeValue { S = mandate.Date.ToString("yyyy-MM-ddTHH:mm:ssZ") } },
                { "value", new AttributeValue { N = mandate.Value.ToString() } },
            };

            var mandateRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = mandateToUpdate
            };

            _ = await _amazonDynamoDBClient.PutItemAsync(mandateRequest);

            // Create transaction
            var transaction = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = "T" } },
                { "sk", new AttributeValue { S = Guid.NewGuid().ToString() } },
                { "mandateId", new AttributeValue { S = closure.MandateId.ToString() } },
                { "clientId", new AttributeValue { S = closure.ClientId.ToString() } },
                { "fundId", new AttributeValue { S = closure.FundId.ToString() } },
                { "operationType", new AttributeValue { N = ((int)closure.OperationType).ToString() } },
                { "date", new AttributeValue { S = closure.Date.ToString("yyyy-MM-ddTHH:mm:ssZ") } },
                { "value", new AttributeValue { N = closure.Value.ToString() } },
            };

            var transactionRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = transaction
            };

            _ = await _amazonDynamoDBClient.PutItemAsync(transactionRequest);

            // Update client balance
            var client = await GetClientByIdAsync(closure.ClientId.ToString());
            client.Balance += closure.Value;

            var clientRecord = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = "C" } },
                { "sk", new AttributeValue { S = client.Id.ToString() } },
                { "balance", new AttributeValue { N = client.Balance.ToString() } },
                { "email", new AttributeValue { S = client.Email } },
                { "name", new AttributeValue { S = client.Name } },
            };

            var clientRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = clientRecord
            };

            _ = await _amazonDynamoDBClient.PutItemAsync(clientRequest);
        }

        public async Task<IEnumerable<IMandate>> GetMandatesAsync()
        {
            var queryRequest = new QueryRequest
            {
                TableName = _tableName,
                KeyConditionExpression = "pk = :v_pk",
                FilterExpression = "#val > :v_value",
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                    { "#val", "value" }
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":v_pk", new AttributeValue { S = "M" } },
                    { ":v_value", new AttributeValue { N = "0" } }
                }
            };

            var response = await _amazonDynamoDBClient.QueryAsync(queryRequest);

            return response.Items.Count > 0 ? TranslateToMandateModel(response.Items) : [];
        }

        public async Task<IEnumerable<ITransaction>> GetTransactionsAsync()
        {
            var queryRequest = new QueryRequest
            {
                TableName = _tableName,
                KeyConditionExpression = "pk = :v_pk",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":v_pk", new AttributeValue { S = "T" } }
                }
            };

            var response = await _amazonDynamoDBClient.QueryAsync(queryRequest);

            return response.Items.Count > 0 ? TranslateToTransactionModel(response.Items) : [];
        }

        private IEnumerable<ITransaction> TranslateToTransactionModel(List<Dictionary<string, AttributeValue>> items)
        {
            var transactions = new List<ITransaction>();

            foreach (var item in items)
            {
                var transaction = new Transaction
                {
                    Id = Guid.Parse(item["sk"].S),
                    ClientId = int.Parse(item["clientId"].S),
                    Date = DateTime.Parse(item["date"].S),
                    FundId = int.Parse(item["fundId"].S),
                    MandateId = Guid.Parse(item["mandateId"].S),
                    OperationType = (Enums.OperationType)int.Parse(item["operationType"].N),
                    Value = decimal.Parse(item["value"].N)
                };

                transactions.Add(transaction);
            }

            return transactions;
        }

        private IEnumerable<IMandate> TranslateToMandateModel(List<Dictionary<string, AttributeValue>> items)
        {
            var mandates = new List<IMandate>();

            foreach (var item in items)
            {
                var mandate = new Mandate
                {
                    Id = Guid.Parse(item["sk"].S),
                    ClientId = int.Parse(item["clientId"].S),
                    Date = DateTime.Parse(item["date"].S),
                    FundId = int.Parse(item["fundId"].S),
                    Value = decimal.Parse(item["value"].N)
                };

                mandates.Add(mandate);
            }

            return mandates;
        }

        private IEnumerable<IFund> TranslateToFundModel(List<Dictionary<string, AttributeValue>> items)
        {
            var funds = new List<IFund>();

            foreach (var item in items)
            {
                var fund = new Fund
                {
                    Id = int.Parse(item["sk"].S),
                    Name = item["name"].S,
                    MinValue = decimal.Parse(item["minValue"].N),
                    Category = item["category"].S
                };

                funds.Add(fund);
            }

            return funds;
        }

        private Client TranslateToClientModel(Document document)
        {
            var client = new Client
            {
                Id = document["sk"].AsInt(),
                Name = document["name"].AsString(),
                Email = document["email"].AsString(),
                Balance = document["balance"].AsDecimal()
            };

            return client;
        }

        private async Task<IMandate> GetMandateByIdAsync(Guid id)
        {
            var table = Table.LoadTable(_amazonDynamoDBClient, _tableName);

            var config = new GetItemOperationConfig
            {
                AttributesToGet = new List<string> { "pk", "sk", "clientId", "date", "fundId", "value" },
                ConsistentRead = true
            };

            var document = await table.GetItemAsync("M", id.ToString(), config);

            var mandate = new Mandate
            {
                Id = document["sk"].AsGuid(),
                ClientId = document["clientId"].AsInt(),
                Date = document["date"].AsDateTime(),
                FundId = document["fundId"].AsInt(),
                Value = document["value"].AsDecimal()
            };

            return mandate;
        }
    }
}
