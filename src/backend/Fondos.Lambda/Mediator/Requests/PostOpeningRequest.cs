using Fondos.Lambda.Mediator.Responses;
using Fondos.Lambda.Models.Interfaces;
using MediatR;

namespace Fondos.Lambda.Mediator.Requests
{
    public class PostOpeningRequest : IRequest<PostOpeningResponse>
    {
        public IOpening Opening { get; set; }
    }
}
