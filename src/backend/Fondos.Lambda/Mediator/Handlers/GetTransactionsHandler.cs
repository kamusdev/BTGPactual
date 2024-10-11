using Amazon.Lambda.Core;
using Fondos.Lambda.DataAccess.Interfaces;
using Fondos.Lambda.Mediator.Requests;
using Fondos.Lambda.Mediator.Responses;
using MediatR;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fondos.Lambda.Mediator.Handlers
{
    public class GetTransactionsHandler : IRequestHandler<GetTransactionsRequest, GetTransactionsResponse>
    {
        private readonly IFondosRepository _fondosRepository;
        
        public GetTransactionsHandler(IFondosRepository fondosRepository)
        {
            _fondosRepository = fondosRepository;
        }

        public async Task<GetTransactionsResponse> Handle(GetTransactionsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var transactions = await _fondosRepository.GetTransactionsAsync();

                return new GetTransactionsResponse
                {
                    Data = transactions,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                LambdaLogger.Log("GetTransactionsHandler Exception:\n");
                LambdaLogger.Log(JsonConvert.SerializeObject(ex));

                return new GetTransactionsResponse
                {
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
