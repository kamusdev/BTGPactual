using Fondos.Lambda.Mediator.Responses;
using MediatR;

namespace Fondos.Lambda.Mediator.Requests
{
    public class GetClientRequest : IRequest<GetClientResponse>
    {
        public string Id { get; set; }
    }
}
