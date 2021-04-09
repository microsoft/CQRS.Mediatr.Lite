using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Exceptions;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Base class for handling a request
    /// </summary>
    /// <typeparam name="Request" cref="IRequest{IResponse}">Represents the request</typeparam>
    /// <typeparam name="Response">Response for the request</typeparam>
    public abstract class RequestHandler<Request, Response> where Request : IRequest
    {   
        protected abstract Task<Response> ProcessRequest(Request request);

        protected string RequestValidationExceptionCustomCode;
        protected string RequestValidationExceptionCustomMessage;

        /// <summary>
        /// Handles the request
        /// </summary>
        /// <param name="request">Request sent to the handler</param>
        /// <returns>Reponse from the handler</returns>
        public virtual async Task<Response> Handle(Request request)
        {
            ValidateRequest(request);
            var response = await ProcessRequest(request);
            return response;
        }

        private void ValidateRequest(Request request)
        {
            var isRequestValid = request.Validate(out string validationErrorMessage);
            if (!isRequestValid)
            {
                throw new RequestValidationException(request.DisplayName, validationErrorMessage, RequestValidationExceptionCustomCode, RequestValidationExceptionCustomMessage);
            }
        }
    }
}
