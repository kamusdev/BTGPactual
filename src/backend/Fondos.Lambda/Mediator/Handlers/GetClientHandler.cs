using Amazon.Lambda.Core;
using Fondos.Lambda.DataAccess.Interfaces;
using Fondos.Lambda.Mediator.Requests;
using Fondos.Lambda.Mediator.Responses;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Fondos.Lambda.Mediator.Handlers
{
    public class GetClientHandler : IRequestHandler<GetClientRequest, GetClientResponse>
    {
        private readonly IFondosRepository _fondosRepository;

        public GetClientHandler(IFondosRepository fondosRepository)
        {
            _fondosRepository = fondosRepository;
        }

        public async Task<GetClientResponse> Handle(GetClientRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _fondosRepository.GetClientByIdAsync(request.Id);

                return new GetClientResponse
                {
                    Data = client,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                LambdaLogger.Log("GetClientHandler Exception:\n");
                LambdaLogger.Log(JsonConvert.SerializeObject(ex));

                return new GetClientResponse
                {
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
