﻿using System.Net;

namespace Fondos.Lambda.Mediator.Responses
{
    public class PostOpeningResponse
    {
        public string Body { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}