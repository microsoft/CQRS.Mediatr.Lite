using CQRS.Mediatr.Lite.Exceptions;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Exceptions
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class RequestValidationExceptionTests
    {
        [TestMethod]
        public void RequestValidationException_ShouldBeCreated_WithValidationMessage()
        {
            #region Arrange
            var mockRequestName = "Mock Request Name";
            var mockValidationMessage = "Mock Validation Message";
            var expectedMessage = $"Request Validataion failed for request {mockRequestName} with message: {mockValidationMessage}";
            var mockExceptionCode = "CODE";
            var mockExceptionMessage = "Message";
            #endregion Arrange

            #region Act
            var requestValidationException = new RequestValidationException(mockRequestName, mockValidationMessage, mockExceptionCode, mockExceptionMessage);
            #endregion Act

            #region Assert
            Assert.IsNotNull(requestValidationException);
            Assert.AreEqual(expectedMessage, requestValidationException.Message);
            Assert.AreEqual(mockExceptionCode, requestValidationException.CustomExceptionCode);
            Assert.AreEqual(mockExceptionMessage, requestValidationException.CustomExceptionMessage);
            Assert.AreEqual(mockValidationMessage, requestValidationException.ValidationErrorMessage);
            #endregion Assert
        }
    }
}
