using Fondos.Lambda.Models.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace Fondos.Lambda.Mediator.Responses
{
    public class GetTransactionsResponse
    {
        public IEnumerable<ITransaction> Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
