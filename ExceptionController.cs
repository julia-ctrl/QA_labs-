using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller
{
    public class ExceptionController
    {
        public int m_criticalExceptions { get; private set; }
        public int m_nonCriticalExeptions { get; private set; }

        static void Main(string[] args)
        { }

        public bool IsCritical(Exception exception)
        {
            var criticalExceptions = new List<Type>()
            {
                typeof(DivideByZeroException),
                typeof(OutOfMemoryException),
                typeof(StackOverflowException),
                typeof(IndexOutOfRangeException),
                typeof(NullReferenceException)
            };

            return criticalExceptions.Contains(exception.GetType());
        }

        public void CountExceptions(Exception exception)
        {
            if(IsCritical(exception))
            {
                m_criticalExceptions++;
            }
            else
            {
                m_nonCriticalExeptions++;
            }
        }
    }
}
