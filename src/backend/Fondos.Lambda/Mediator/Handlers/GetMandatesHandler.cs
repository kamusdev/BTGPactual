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
    public class GetMandatesHandler : IRequestHandler<GetMandatesRequest, GetMandatesResponse>
    {
        private readonly IFondosRepository _fondosRepository;
        
        public GetMandatesHandler(IFondosRepository fondosRepository)
        {
            _fondosRepository = fondosRepository;
        }

        public async Task<GetMandatesResponse> Handle(GetMandatesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var mandates = await _fondosRepository.GetMandatesAsync();

                return new GetMandatesResponse
                {
                    Data = mandates,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                LambdaLogger.Log("GetMandatesHandler Exception:\n");
                LambdaLogger.Log(JsonConvert.SerializeObject(ex));

                return new GetMandatesResponse
                {
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
