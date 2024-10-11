﻿using Amazon.Lambda.Core;
using Fondos.Lambda.DataAccess.Interfaces;
using Fondos.Lambda.Mediator.Requests;
using Fondos.Lambda.Mediator.Responses;
using MediatR;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fondos.Lambda.Mediator.Handlers
{
    public class GetFundsHandler : IRequestHandler<GetFundsRequest, GetFundsResponse>
    {
        private readonly IFondosRepository _fondosRepository;
        
        public GetFundsHandler(IFondosRepository fondosRepository)
        {
            _fondosRepository = fondosRepository;
        }

        public async Task<GetFundsResponse> Handle(GetFundsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var funds = await _fondosRepository.GetFundsAsync();

                return new GetFundsResponse
                {
                    Data = funds,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                LambdaLogger.Log("GetFundsHandler Exception:\n");
                LambdaLogger.Log(JsonConvert.SerializeObject(ex));

                return new GetFundsResponse
                {
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
