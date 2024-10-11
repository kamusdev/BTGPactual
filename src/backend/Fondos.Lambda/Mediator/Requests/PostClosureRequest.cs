using Fondos.Lambda.Mediator.Responses;
using Fondos.Lambda.Models.Interfaces;
using MediatR;

namespace Fondos.Lambda.Mediator.Requests
{
    public class PostClosureRequest : IRequest<PostClosureResponse>
    {
        public IClosure Closure { get; set; }
    }
}
