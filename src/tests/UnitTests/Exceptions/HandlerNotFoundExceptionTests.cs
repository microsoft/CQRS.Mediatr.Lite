using CQRS.Mediatr.Lite.Exceptions;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Exceptions
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class HandlerNotFoundExceptionTests
    {
        [TestMethod]
        public void HandlerNotFoundException_ShouldBeCreated()
        {
            #region Arrange
            var mockHandlerType = typeof(MockCommandHandler);
            var expectedMessage = $"No handler of type {mockHandlerType.FullName} was found. Please ensure that the handler has been registered in your dependency resolution.";
            #endregion Arrange

            #region Act
            var handlerNotFoundException = new HandlerNotFoundException(mockHandlerType);
            #endregion Act

            #region Assert
            Assert.IsNotNull(handlerNotFoundException);
            Assert.AreEqual(expectedMessage, handlerNotFoundException.Message);
            #endregion Assert
        }
    }
}
