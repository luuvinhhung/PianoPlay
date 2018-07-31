using PianoPlay_BE.ViewModels;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace PianoPlay_BE.Infrastructure
{
    public class ErrorActionResult : IHttpActionResult
    {
        private HttpResponseMessage _response;

        public ErrorActionResult(HttpRequestMessage request, HttpStatusCode statusCode, ErrorModel errors)
        {
            _response = request.CreateResponse(statusCode, errors);
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }

        public HttpResponseMessage GetResponse()
        {
            return _response;
        }
    }
}