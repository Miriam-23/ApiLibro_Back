using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Web;

namespace ApiLibro.Handlers
{
    public class PreflightRequestsHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Maneja solicitudes OPTIONS (CORS preflight)
            if (request.Method == HttpMethod.Options)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                return Task.FromResult(response);
            }//if
            return base.SendAsync(request, cancellationToken);
        }
    }
}