using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Exceptions;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using CQRS.Mediatr.Lite.Tests.Common.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Query
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class QueryHandlerTests
    {
        [TestMethod]
        public async Task QueryHandler_ShouldHandleQuery()
        {
            #region Arrange
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            var queryHandler = new MockQueryHandler();
            #endregion  Arrange

            #region Act
            var mockResponse = await queryHandler.Handle(query);
            #endregion  Act

            #region Assert
            Assert.IsNotNull(mockResponse);
            Assert.AreEqual(queryId, mockResponse.RequestId);
            #endregion Assert
        }

        [ExpectedException(typeof(RequestValidationException))]
        [TestMethod]
        public void QueryHandler_ShouldThrowRequestValidationException_WhenQueryValidationFails()
        {
            #region Arrange
            var queryId = Guid.NewGuid().ToString();
            var mockValidationMessage = "Dummy Validation Failed";
            var query = new MockQuery(queryId, mockValidationMessage);
            var queryHandler = new MockQueryHandler();
            #endregion  Arrange

            #region Act
            AssertExtender.AssertRaisesException<RequestValidationException>(
                () => queryHandler.Handle(query).Wait(),
                expectedMessage: $"Request Validataion failed for request {query.DisplayName} with message: {mockValidationMessage}");
            #endregion Act
        }
    }
}
