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
    public class PostOpeningHandler : IRequestHandler<PostOpeningRequest, PostOpeningResponse>
    {
        private readonly IFondosRepository _fondosRepository;

        public PostOpeningHandler(IFondosRepository fondosRepository)
        {
            _fondosRepository = fondosRepository;
        }

        public async Task<PostOpeningResponse> Handle(PostOpeningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _fondosRepository.PostOpeningAsync(request.Opening);

                return new PostOpeningResponse
                {
                    Body = JsonConvert.SerializeObject(new { Message = "Opening created succesfully!" }),
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                LambdaLogger.Log("PostOpeningHandler Exception:\n");
                LambdaLogger.Log(JsonConvert.SerializeObject(ex));

                return new PostOpeningResponse
                {
                    Body = JsonConvert.SerializeObject(new { Message = "There was an error cretaing the opening!" }),
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
