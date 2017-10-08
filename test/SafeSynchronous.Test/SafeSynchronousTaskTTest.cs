using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace SafeSynchronous.Test
{
    public class SafeSynchronousTaskTTest : SafeSynchronousTaskTestBase
    {
        [Fact]
        public void RunTaskT()
        {
            async Task<bool> SampleTask() => await Task.FromResult(true);

            var result = SafeSynchronousTask.Run(SampleTask);

            Assert.True(result);
            VerifyContextCalled(Times.Never);
        }

        [Fact]
        public void RunTaskTUnwrapException()
        {
            Assert.Throws<Exception>(() => SafeSynchronousTask.Run(SampleFaultedTaskT));
        }

        [Fact]
        public void EnsureTaskTTestSetupCorectly()
        {
            var result = SampleTaskT().Result;

            Assert.True(result);
            VerifyContextCalled(Times.Once);
        }

        [Fact]
        public void EnsureFaultedTaskTTestSetupCorectly()
        {
            Assert.Throws<AggregateException>(() => SampleFaultedTaskT().Wait());
        }

        private static async Task<bool> SampleTaskT()
        {
            await Task.Yield();
            return true;
        }

        private static Task<bool> SampleFaultedTaskT() => Task.FromException<bool>(new Exception());
    }
}