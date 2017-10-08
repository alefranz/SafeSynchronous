using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace SafeSynchronous.Test
{
    public class SafeSynchronousTaskTest : SafeSynchronousTaskTestBase
    {
        [Fact]
        public void RunTask()
        {
            SafeSynchronousTask.Run(SampleTask);

            VerifyContextCalled(Times.Never);
        }

        [Fact]
        public void RunTaskUnwrapException()
        {
            Assert.Throws<Exception>(() => SafeSynchronousTask.Run(SampleFaultedTask));
        }

        [Fact]
        public void EnsureTaskTestSetupCorectly()
        {
            async Task SampleTask() => await Task.Yield();

            SampleTask().Wait();

            VerifyContextCalled(Times.Once);
        }

        [Fact]
        public void EnsureFaultedTestSetupCorectly()
        {
            Assert.Throws<AggregateException>(() => SampleFaultedTask().Wait());
        }

        private static async Task SampleTask() => await Task.Yield();
        private static Task SampleFaultedTask() => Task.FromException(new Exception());
    }
}
