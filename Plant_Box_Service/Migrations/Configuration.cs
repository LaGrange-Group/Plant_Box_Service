namespace Plant_Box_Service.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Plant_Box_Service.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Plant_Box_Service.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.States.AddOrUpdate(
                s => s.Name,
                new Models.State { Name = "AK" },
                new Models.State { Name = "AL" },
                new Models.State { Name = "AR" },
                new Models.State { Name = "AZ" },
                new Models.State { Name = "CA" },
                new Models.State { Name = "CO" },
                new Models.State { Name = "CT" },
                new Models.State { Name = "DE" },
                new Models.State { Name = "FL" },
                new Models.State { Name = "GA" },
                new Models.State { Name = "HI" },
                new Models.State { Name = "IA" },
                new Models.State { Name = "ID" },
                new Models.State { Name = "IL" },
                new Models.State { Name = "IN" },
                new Models.State { Name = "KS" },
                new Models.State { Name = "LA" },
                new Models.State { Name = "MA" },
                new Models.State { Name = "MD" },
                new Models.State { Name = "ME" },
                new Models.State { Name = "MI" },
                new Models.State { Name = "MN" },
                new Models.State { Name = "MO" },
                new Models.State { Name = "MS" },
                new Models.State { Name = "MT" },
                new Models.State { Name = "NC" },
                new Models.State { Name = "ND" },
                new Models.State { Name = "NE" },
                new Models.State { Name = "NH" },
                new Models.State { Name = "NJ" },
                new Models.State { Name = "NM" },
                new Models.State { Name = "NV" },
                new Models.State { Name = "NY" },
                new Models.State { Name = "OH" },
                new Models.State { Name = "OK" },
                new Models.State { Name = "OR" },
                new Models.State { Name = "PA" },
                new Models.State { Name = "RI" },
                new Models.State { Name = "AL" },
                new Models.State { Name = "SC" },
                new Models.State { Name = "SD" },
                new Models.State { Name = "TN" },
                new Models.State { Name = "TX" },
                new Models.State { Name = "UT" },
                new Models.State { Name = "VA" },
                new Models.State { Name = "VT" },
                new Models.State { Name = "WA" },
                new Models.State { Name = "WI" },
                new Models.State { Name = "WV" },
                new Models.State { Name = "WY" }
                );
        }
    }
}
