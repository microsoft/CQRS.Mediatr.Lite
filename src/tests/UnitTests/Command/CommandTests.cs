using System;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Command
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void Command_ShouldBeCreated()
        {
            #region Arrange
            var mockCommandId = Guid.NewGuid().ToString();
            var correlationId = Guid.NewGuid().ToString();
            var transactionId = Guid.NewGuid().ToString();
            var mockDomain = "Mock Domain";
            var createdOn = DateTime.UtcNow;
            #endregion Arrange

            #region Act
            var mockCommand = new MockCommand(mockCommandId)
            {
                CorrelationId = correlationId,
                TransactionId = transactionId,
                Domain = mockDomain,
                IssuedAt = createdOn
            };
            #endregion Act

            #region Assert
            Assert.IsNotNull(mockCommand);
            Assert.IsNotNull(mockCommand.DisplayName);
            Assert.AreEqual(mockCommandId, mockCommand.Id);
            Assert.AreEqual(correlationId, mockCommand.CorrelationId);
            Assert.AreEqual(transactionId, mockCommand.TransactionId);
            Assert.AreEqual(mockDomain, mockCommand.Domain);
            Assert.AreEqual(createdOn, mockCommand.IssuedAt);
            #endregion Assert
        }
    }
}
