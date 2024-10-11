using Fondos.Lambda.Models.Interfaces;
using System.Net;

namespace Fondos.Lambda.Mediator.Responses
{
    public class GetClientResponse
    {
        public IClient Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
