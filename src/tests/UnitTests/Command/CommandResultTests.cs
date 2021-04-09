using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Command
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CommandResultTests
    {
        [TestMethod]
        public void IdCommandResult_ShouldBeCreated()
        {
            #region Arrange
            var commandId = Guid.NewGuid().ToString();
            var timeTaken = TimeSpan.FromSeconds(3);
            var executedAt = DateTime.UtcNow;
            #endregion Arrange

            #region Act
            var result = new IdCommandResult(commandId)
            {
                TimeTaken = timeTaken,
                ExecutedAt = executedAt
            };
            #endregion Act

            #region Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccesfull);
            Assert.AreEqual(commandId, result.Id);
            Assert.AreEqual(timeTaken, result.TimeTaken);
            Assert.AreEqual(executedAt, result.ExecutedAt);
            #endregion Assert
        }
    }
}
