using System;
using System.Threading;
using Moq;

namespace SafeSynchronous.Test
{
    public class SafeSynchronousTaskTestBase : IDisposable
    {
        private readonly Mock<SynchronizationContext> _synchronizationContext;
        private readonly SynchronizationContext _testFrameworkSynchronizationContext;

        public SafeSynchronousTaskTestBase()
        {
            _synchronizationContext = new Mock<SynchronizationContext> {CallBase = true};

            _testFrameworkSynchronizationContext = SynchronizationContext.Current;
            SynchronizationContext.SetSynchronizationContext(_synchronizationContext.Object);
        }

        protected void VerifyContextCalled(Func<Times> times)
        {
            _synchronizationContext.Verify(context => context.Post(It.IsAny<SendOrPostCallback>(), It.IsAny<object>()),
                times);
        }

        public void Dispose()
        {
            SynchronizationContext.SetSynchronizationContext(_testFrameworkSynchronizationContext);
        }
    }
}