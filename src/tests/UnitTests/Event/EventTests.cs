using System;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Event
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void Event_ShouldBeCreated()
        {
            #region Arrange
            var mockEventId = Guid.NewGuid().ToString();
            var correlationId = Guid.NewGuid().ToString();
            var transactionId = Guid.NewGuid().ToString();
            var mockGenerator = "Mock Generator";
            var createdOn = DateTime.UtcNow;
            var mockVersion = "V1";
            #endregion Arrange

            #region Act
            var mockEvent = new MockEvent(mockEventId)
            {
                CorrelationId = correlationId,
                TransactionId = transactionId,
                GeneratedBy = mockGenerator,
                GeneratedOn = createdOn,
                Version = mockVersion
            };
            #endregion Act

            #region Assert
            Assert.IsNotNull(mockEvent);
            Assert.IsNotNull(mockEvent.DisplayName);
            Assert.AreEqual(mockEventId, mockEvent.Id);
            Assert.AreEqual(correlationId, mockEvent.CorrelationId);
            Assert.AreEqual(transactionId, mockEvent.TransactionId);
            Assert.AreEqual(mockGenerator, mockEvent.GeneratedBy);
            Assert.AreEqual(createdOn, mockEvent.GeneratedOn);
            Assert.AreEqual(mockVersion, mockEvent.Version);
            #endregion Assert
        }
    }
}
