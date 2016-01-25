namespace StudentSystem.WebApi
{
    using StudentSystem.Data;
    using StudentSystem.Data.Migrations;
    using System.Data.Entity;

    public class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemDbContext, Configuration>());

            StudentSystemDbContext.Create().Database.Initialize(true);
        }
    }
}