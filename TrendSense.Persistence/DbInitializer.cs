namespace TrendSense.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
