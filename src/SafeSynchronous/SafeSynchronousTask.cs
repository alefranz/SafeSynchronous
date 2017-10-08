using System;
using System.Threading;
using System.Threading.Tasks;

namespace SafeSynchronous
{
    public class SafeSynchronousTask
    {
        public static void Run(Func<Task> function)
        {
            var context = SynchronizationContext.Current;
            SynchronizationContext.SetSynchronizationContext(null);

            try
            {
                function().GetAwaiter().GetResult();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(context);
            }
        }

        public static T Run<T>(Func<Task<T>> function)
        {
            var context = SynchronizationContext.Current;
            SynchronizationContext.SetSynchronizationContext(null);

            try
            {
                return function().GetAwaiter().GetResult();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(context);
            }
        }
    }
}
