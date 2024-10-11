using Fondos.Lambda.Models.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace Fondos.Lambda.Mediator.Responses
{
    public class GetFundsResponse
    {
        public IEnumerable<IFund> Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
