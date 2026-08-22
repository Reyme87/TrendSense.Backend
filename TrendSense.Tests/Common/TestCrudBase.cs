namespace TrendSense.Tests.Common
{
    public class TestCrudBase : IDisposable
    {
        public TrendSenseContextFactory Context;

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
