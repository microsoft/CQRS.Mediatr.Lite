using CQRS.Mediatr.Lite.SDK.Domain;
using CQRS.Mediatr.Lite.Tests.Mocks;
using CQRS.Mediatr.Lite.Tests.Mocks.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Domain
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AggregateRootUnitTests
    {
        [TestMethod]
        public void AggregateRoot_ShouldGetCreated_WithId()
        {
            #region Arrange
            string id = Guid.NewGuid().ToString();
            #endregion Arrange

            #region Act
            AggregateRoot aggRoot = new MockAggregateRoot(id);
            #endregion Act

            #region Assert
            Assert.IsNotNull(aggRoot);
            Assert.AreEqual(id, aggRoot.Id);
            #endregion Assert
        }

        [TestMethod]
        public void AggregateRoot_ShouldApplyChange()
        {
            #region Arrange
            string id = Guid.NewGuid().ToString();
            string eventId = Guid.NewGuid().ToString();
            MockEvent @event = new MockEvent(eventId);
            #endregion Arrange

            #region Act
            AggregateRoot aggRoot = new MockAggregateRoot(id);
            aggRoot.ApplyChange(@event);
            List<CQRS.Mediatr.Lite.Event> uncomittedEvents = aggRoot.GetUncommittedChanges().ToList();
            #endregion Act

            #region Assert
            Assert.IsNotNull(uncomittedEvents);
            Assert.IsTrue(uncomittedEvents.Any());
            Assert.AreEqual(eventId, uncomittedEvents.First().Id);
            #endregion Assert
        }

        [TestMethod]
        public async Task AggregateRoot_ShouldCommitEvent_ToEventBus()
        {
            #region Arrange
            string id = Guid.NewGuid().ToString();
            string eventId = Guid.NewGuid().ToString();
            MockEvent @event = new(eventId);
            Mock<IEventBus> mockEventBus = new();
            mockEventBus.Setup(bus => bus.Send(It.IsAny<IEnumerable<CQRS.Mediatr.Lite.Event>>()))
                .Returns(Task.CompletedTask);
            
            #endregion Arrange

            #region Act
            AggregateRoot aggRoot = new MockAggregateRoot(id);
            aggRoot.ApplyChange(@event);
            await aggRoot.Commit(mockEventBus.Object);
            List<CQRS.Mediatr.Lite.Event> uncomittedEvents = aggRoot.GetUncommittedChanges().ToList();
            #endregion Act

            #region Assert
            mockEventBus.Verify(bus => bus.Send(It.IsAny<IEnumerable<CQRS.Mediatr.Lite.Event>>()), Times.Once);
            Assert.IsFalse(uncomittedEvents.Any());
            #endregion Assert
        }
    }
}
