namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres VALUES (1, 'Comedy')");
            Sql("INSERT INTO Genres VALUES (2, 'Action')");
            Sql("INSERT INTO Genres VALUES (3, 'Romance')");
            Sql("INSERT INTO Genres VALUES (4, 'Family')");
        }
        
        public override void Down()
        {
        }
    }
}
