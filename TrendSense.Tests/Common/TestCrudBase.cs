using TrendSense.Persistence;

namespace TrendSense.Tests.Common
{
    public class TestCrudBase : IDisposable
    {
        public AppDbContext Context;

        public TestCrudBase()
        {
            Context = TrendSenseContextFactory.Create();
        }

        public void Dispose()
        {
            TrendSenseContextFactory.Destroy(Context);
        }
    }
}
