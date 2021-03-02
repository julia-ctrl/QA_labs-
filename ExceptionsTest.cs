using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ExceptionsTest
    {
        [TestCase(typeof(DivideByZeroException), true)]
        [TestCase(typeof(OutOfMemoryException), true)]
        [TestCase(typeof(StackOverflowException), true)]
        [TestCase(typeof(IndexOutOfRangeException), true)]
        [TestCase(typeof(NullReferenceException), true)]
        [TestCase(typeof(AccessViolationException), false)]
        [TestCase(typeof(IndexOutOfRangeException), false)]
        [TestCase(typeof(InvalidOperationException), false)]
        [TestCase(typeof(InsufficientMemoryException), false)]
        [TestCase(typeof(InsufficientExecutionStackException), false)]
        [TestCase(typeof(ArgumentNullException), false)]
        [TestCase(typeof(ArgumentOutOfRangeException), false)]
        [TestCase(typeof(NullReferenceException), false)]
        public void IsCritical_WhenCalled_ShouldCheckException(Type exceptionType, bool expectedResult)
        {
            var instance = (Exception)Activator.CreateInstance(exceptionType);
            try
            {
                throw instance;
            }
            catch (Exception e)
            {
                Assert.AreEqual(expectedResult, new Controller.ExceptionController().IsCritical(e));
                return;
            }
        }

        [Test]
        public void CountExceptions_WhenCalled_ShouldCounterValuesCorrect()
        {
            var ExceptionsList = new List<Type>()
            {
                    // critical
                    typeof(DivideByZeroException),
                    typeof(OutOfMemoryException),
                    typeof(StackOverflowException),
                    typeof(IndexOutOfRangeException),
                    typeof(NullReferenceException),
                    // non critical
                    typeof(AccessViolationException),
                    typeof(IndexOutOfRangeException),
                    typeof(InvalidOperationException),
                    typeof(InsufficientMemoryException),
                    typeof(InsufficientExecutionStackException),
                    typeof(ArgumentNullException),
                    typeof(ArgumentOutOfRangeException),
                    typeof(NullReferenceException)
            };

            var controller = new Controller.ExceptionController();

            foreach (var item in ExceptionsList)
            {
                var instance = (Exception)Activator.CreateInstance(item);
                controller.CountExceptions(instance);
            }

            Assert.AreEqual(controller.m_criticalExceptions, 5);
            Assert.AreEqual(controller.m_nonCriticalExeptions, 8);
        }

        [Test]
        public void CountExceptions__WhenCalledWithInitCounters_ShouldReturnZero()
        {
            var controller = new Controller.ExceptionController();

            Assert.AreEqual(controller.m_criticalExceptions, 0);
            Assert.AreEqual(controller.m_nonCriticalExeptions, 0);
        }
    }
}