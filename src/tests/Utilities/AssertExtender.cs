using System;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.Common.TestHelper
{
    /// <summary>
    /// Extends the Assert functionality
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AssertExtender
    {
        /// <summary>
        /// Runs the statement that should cause the expected exception to be thrown and then verifies that the correct exception has been thrown
        /// </summary>
        /// <typeparam name="TException">Type of the exception that is expected</typeparam>
        /// <param name="action">Action containing the code that will throw the exception</param>
        /// <param name="expectedMessage">Expected message in the</param>
        /// <param name="expectedInnerException">Expected exception in the inner body of the exception</param>
        public static void AssertRaisesException<TException>(Action action, string expectedMessage, Exception expectedInnerException)
            where TException : Exception
        {
            try
            {
                action();
                Assert.Fail($"Expected exception not thrown. Expected an exception of type {typeof(TException)}");
            }
            catch (TException exception)
            {
                AssertException<TException>(exception, expectedMessage, expectedInnerException);
                throw exception;
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerExceptions.Count == 1 && aggregateException.InnerExceptions.First() is TException)
                {
                    var exception = aggregateException.InnerExceptions.First() as TException;
                    AssertException<TException>(exception, expectedMessage, expectedInnerException);
                    throw exception;
                }
                Assert.Fail($"Exception of type {typeof(TException)} was expected but received {aggregateException.GetType()}");
            }
            catch (Exception exception)
            {
                Assert.Fail($"Exception of type {typeof(TException)} was expected but received {exception.GetType()}");
            }
        }

        /// <summary>
        /// Runs the statement that should cause the expected exception to be thrown and then verifies that the correct exception has been thrown
        /// </summary>
        /// <typeparam name="TException">Type of the exception that is expected</typeparam>
        /// <param name="action">Action containing the code that will throw the exception</param>
        /// <param name="expectedMessage">Expected message in the</param>
        public static void AssertRaisesException<TException>(Action action, string expectedMessage)
            where TException : Exception
        {
            AssertRaisesException<TException>(action, expectedMessage, null);
        }

        private static void AssertException<TException>(TException exception, string expectedMessage, Exception expectedInnerException)
            where TException : Exception
        {
            Assert.AreEqual(expectedMessage, exception.Message);
            if (expectedInnerException == null)
                Assert.IsNull(exception.InnerException);
            else
                Assert.AreEqual(expectedInnerException, exception.InnerException);
        }
    }
}
