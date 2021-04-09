using System;

namespace CQRS.Mediatr.Lite.Exceptions
{
    /// <summary>
    /// Exception when the request in Invalid
    /// </summary>
    [Serializable]
    public class RequestValidationException: Exception
    {
        public string ValidationErrorMessage { get; set; }
        public string CustomExceptionCode { get; set; }
        public string CustomExceptionMessage { get; set; }
        /// <summary>
        /// Constructs the Request Validation Exception
        /// </summary>
        /// <param name="requestName">Name of the request that has failed validation</param>
        /// <param name="validationMessage">The message related to the validation failure</param>
        public RequestValidationException(string requestName, string validationMessage, string customCode, string customExceptionMessage)
            :base($"Request Validataion failed for request {requestName} with message: {validationMessage}")
        {
            ValidationErrorMessage = validationMessage;
            CustomExceptionCode = customCode;
            CustomExceptionMessage = customExceptionMessage;
        }
    }
}
