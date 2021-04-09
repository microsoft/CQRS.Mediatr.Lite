using System;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Query
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void Query_ShouldBeCreated()
        {
            #region Arrange
            var mockQueryId = Guid.NewGuid().ToString();
            var correlationId = Guid.NewGuid().ToString();
            var transactionId = Guid.NewGuid().ToString();
            #endregion Arrange

            #region Act
            var mockQuery = new MockQuery(mockQueryId)
            {
                CorrelationId = correlationId,
                TransactionId = transactionId
            };
            #endregion Act

            #region Assert
            Assert.IsNotNull(mockQuery);
            Assert.IsNotNull(mockQuery.DisplayName);
            Assert.AreEqual(mockQueryId, mockQuery.Id);
            Assert.AreEqual(correlationId, mockQuery.CorrelationId);
            Assert.AreEqual(transactionId, mockQuery.TransactionId);
            #endregion Assert
        }

    }
}
