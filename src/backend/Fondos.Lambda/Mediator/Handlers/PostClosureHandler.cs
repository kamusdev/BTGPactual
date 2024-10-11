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
    public class PostClosureHandler : IRequestHandler<PostClosureRequest, PostClosureResponse>
    {
        private readonly IFondosRepository _fondosRepository;

        public PostClosureHandler(IFondosRepository fondosRepository)
        {
            _fondosRepository = fondosRepository;
        }

        public async Task<PostClosureResponse> Handle(PostClosureRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _fondosRepository.PostClosureAsync(request.Closure);

                return new PostClosureResponse
                {
                    Body = JsonConvert.SerializeObject(new { Message = "Closure created succesfully!" }),
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                LambdaLogger.Log("PostClosureHandler Exception:\n");
                LambdaLogger.Log(JsonConvert.SerializeObject(ex));

                return new PostClosureResponse
                {
                    Body = JsonConvert.SerializeObject(new { Message = "There was an error cretaing the closure!" }),
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
