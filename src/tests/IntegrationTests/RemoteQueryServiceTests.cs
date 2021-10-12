using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using CQRS.Mediatr.Lite.Exceptions;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class RemoteQueryServiceTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task RemoteQueryService_ShouldQueryResult_WhenQueryIsSent_Integration()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            var queryDictionary = new Dictionary<string, Tuple<Type, Type>>()
            {
                { query.DisplayName, new Tuple<Type, Type>(typeof(MockQuery), typeof(MockQueryResponse)) }
            };
            var queryService = new RemoteQueryService(handlerResolver, queryDictionary);
            #endregion Arrange

            #region Act
            var response = await queryService.Query(query.DisplayName, JsonConvert.SerializeObject(query));
            MockQueryResponse queryResponse = response as MockQueryResponse;
            #endregion Act

            #region Assert
            Assert.IsNotNull(queryResponse);
            #endregion Assert
        }


        [TestCategory("Integration")]
        [TestMethod]
        [ExpectedException(typeof(HandlerNotFoundException))]
        public async Task RemoteQueryService_ShouldThrowException_WhenQueryNameIsNotRegistered()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            var queryDictionary = new Dictionary<string, Tuple<Type, Type>>()
            {
                { query.DisplayName, new Tuple<Type, Type>(typeof(MockQuery), typeof(MockQueryResponse)) }
            };
            var queryService = new RemoteQueryService(handlerResolver, queryDictionary);
            #endregion Arrange

            #region Act & Assert
            _ = await queryService.Query(Guid.NewGuid().ToString(), JsonConvert.SerializeObject(query));
            #endregion Act & Assert
        }
    }
}
