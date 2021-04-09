using System;
using CQRS.Mediatr.Lite.Exceptions;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Exceptions
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class HandlerResolutionExceptionTests
    {
        [TestMethod]
        public void HandlerResolutionException_ShouldBeCreated_WithHandlerTypeAndException()
        {
            #region Arrange
            var mockHandlerType = typeof(MockQueryHandler);
            var dummyException = new Exception("Dummy Exception");
            var expectedMessage = $"There was an error in creating the handler of type {mockHandlerType.FullName}. Please check the dependency resolution module.";
            #endregion Arrange

            #region Act
            var handlerResolutionException = new HandlerResolutionException(mockHandlerType, dummyException);
            #endregion Act

            #region Assert
            Assert.IsNotNull(handlerResolutionException);
            Assert.AreEqual(expectedMessage, handlerResolutionException.Message);
            Assert.AreEqual(dummyException.ToString(), handlerResolutionException.InnerException.ToString());
            #endregion Assert
        }
    }
}
