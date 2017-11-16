namespace CodeFirts_MigrationData.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodeFirts_MigrationData.Program.MyContext>
    {
        public Configuration()
        {
            #region methode 1: migration automatique

            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            #endregion

            #region methode2:migration basée surle code

            AutomaticMigrationsEnabled = false;

            #endregion

            //ContextKey = "CodeFirts_MigrationData.MyContext";
        }

        protected override void Seed(CodeFirts_MigrationData.Program.MyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
