using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Internal;
using CQRS.Mediatr.Lite.Exceptions;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using CQRS.Mediatr.Lite.Tests.Common.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Internal
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class QueryHandlerWrapperTests
    {
        [TestMethod]
        public async Task QueryHandlerWrapper_ShouldHandleQuery()
        {
            #region Arrange
            var queryId = Guid.NewGuid().ToString();
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var queryHandlerWrapper = new QueryHandlerWrapper<MockQuery, MockQueryResponse>();
            var query = new MockQuery(queryId);
            #endregion Arrange

            #region Act
            var response = await queryHandlerWrapper.Handle(query, handlerResolver);
            #endregion Act

            #region Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(queryId, response.RequestId);
            #endregion Assert
        }

        [ExpectedException(typeof(HandlerNotFoundException))]
        [TestMethod]
        public void QueryHandlerWrapper_ShouldThrowHandlerNotFoundException_WhenNoQueryHandlerIsFound()
        {
            #region Arrange
            var queryId = Guid.NewGuid().ToString();
            var mockHandlerGenerator = new MockHandlerGenerator()
            {
                _queryHandler = null
            };
            var handlerResolver = new MockRequestHandlerResolver(mockHandlerGenerator);
            var queryHandlerWrapper = new QueryHandlerWrapper<MockQuery, MockQueryResponse>();
            var query = new MockQuery(queryId);
            #endregion Arrange

            #region Act
            AssertExtender.AssertRaisesException<HandlerNotFoundException>(
                () => queryHandlerWrapper.Handle(query, handlerResolver).Wait(),
                expectedMessage: $"No handler of type {typeof(QueryHandler<MockQuery, MockQueryResponse>).FullName} was found. Please ensure that the handler has been registered in your dependency resolution.");
            #endregion Act
        }
    }
}
