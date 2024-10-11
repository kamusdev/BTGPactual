using Amazon.DynamoDBv2;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Fondos.Lambda.DataAccess;
using Fondos.Lambda.DataAccess.Interfaces;
using Fondos.Lambda.Mediator.Requests;
using Fondos.Lambda.Models;
using Fondos.Lambda.Models.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace FondosLambda
{

    public class EntryPoint
    {
        private IServiceCollection ServiceCollection { get; set; }
        protected virtual IServiceProvider ServiceProvider { get; private set; }

        private Dictionary<string, string> _headers;

        public EntryPoint()
        {

            _headers = new Dictionary<string, string> {
                            { "Content-Type", "application/json" },
                            { "Access-Control-Allow-Origin", "*" }, // Permitir todos los orígenes
                            { "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" }, // Métodos permitidos
                            { "Access-Control-Allow-Headers", "Content-Type, Authorization" }
                        };

            ServiceCollection = new ServiceCollection();
            BuildServiceCollection();
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
        public virtual async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request, ILambdaContext context)
        {
            LambdaLogger.Log(JsonConvert.SerializeObject(request));

            var mediator = ServiceProvider.GetRequiredService<IMediator>();

            return request.HttpMethod.ToUpper() switch
            {
                "POST" => await Post(request, mediator),
                "GET" => await Get(request, mediator),
            };
        }

        private async Task<APIGatewayProxyResponse> Post(APIGatewayProxyRequest request, IMediator mediator)
        {
            switch (request.Resource.ToLower())
            {
                case "/opening":
                    return await PostOpening(request, mediator);
                case "/closure":
                    return await PostClosure(request, mediator);
                default:
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 404,
                        Body = null,
                        Headers = _headers
                    };
            }
        }

        private async Task<APIGatewayProxyResponse> PostOpening(APIGatewayProxyRequest request, IMediator mediator)
        {
            var model = JsonConvert.DeserializeObject<Opening>(request.Body);

            var postRequest = new PostOpeningRequest
            {
                 Opening = model,
            };

            var result = await mediator.Send(postRequest);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)result.StatusCode,
                Body = result.Body,
                Headers = _headers
            };
        }

        private async Task<APIGatewayProxyResponse> PostClosure(APIGatewayProxyRequest request, IMediator mediator)
        {
            var model = JsonConvert.DeserializeObject<Closure>(request.Body);

            var postRequest = new PostClosureRequest
            {
                Closure = model,
            };

            var result = await mediator.Send(postRequest);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)result.StatusCode,
                Body = result.Body,
                Headers = _headers
            };
        }

        private async Task<APIGatewayProxyResponse> Get(APIGatewayProxyRequest request, IMediator mediator)
        {
            switch (request.Resource.ToLower())
            {
                case "/clients/{id}":
                    return await GetCientById(request.PathParameters["id"], mediator);
                case "/funds":
                    return await GetFunds(request, mediator);
                case "/mandates":
                    return await GetMandates(request, mediator);
                case "/transactions":
                    return await GetTransactions(request, mediator);
                default:
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 404,
                        Body = null,
                        Headers = _headers
                    };
            }
        }

        private async Task<APIGatewayProxyResponse> GetFunds(APIGatewayProxyRequest request, IMediator mediator)
        {
            var getRequest = new GetFundsRequest();

            var result = await mediator.Send(getRequest);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)result.StatusCode,
                Body = JsonConvert.SerializeObject(result.Data),
                Headers = _headers
            };
        }

        private async Task<APIGatewayProxyResponse> GetMandates(APIGatewayProxyRequest request, IMediator mediator)
        {
            var getRequest = new GetMandatesRequest();

            var result = await mediator.Send(getRequest);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)result.StatusCode,
                Body = JsonConvert.SerializeObject(result.Data),
                Headers = _headers
            };
        }

        private async Task<APIGatewayProxyResponse> GetTransactions(APIGatewayProxyRequest request, IMediator mediator)
        {
            var getRequest = new GetTransactionsRequest();

            var result = await mediator.Send(getRequest);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)result.StatusCode,
                Body = JsonConvert.SerializeObject(result.Data),
                Headers = _headers
            };
        }

        protected virtual async Task<APIGatewayProxyResponse> GetCientById(string id, IMediator mediator)
        {
            var getRequest = new GetClientRequest { Id = id };

            var result = await mediator.Send(getRequest);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)result.StatusCode,
                Body = JsonConvert.SerializeObject(result.Data),
                Headers = _headers
            };
        }

        private void BuildServiceCollection()
        {
            ServiceCollection
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddScoped<IAmazonDynamoDB, AmazonDynamoDBClient>()
                .AddScoped<IFondosRepository, FondosRepository>();

            ServiceProvider = ServiceCollection.BuildServiceProvider();
            
        }     
    }
}
