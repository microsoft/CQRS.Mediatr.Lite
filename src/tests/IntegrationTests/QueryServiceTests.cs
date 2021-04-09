using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class QueryServiceTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task QueryService_ShouldQueryResult_WhenQueryIsSent_Integration()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var queryService = new QueryService(handlerResolver);
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            #endregion Arrange

            #region Act
            var response = await queryService.Query(query);
            #endregion Act

            #region Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(queryId, response.RequestId);

            mockHandlerGenerator.VerifyGlobalProcessors(queryId);
            mockHandlerGenerator.VerifyQueryProcessors(queryId);
            #endregion Assert
        }
    }
}
