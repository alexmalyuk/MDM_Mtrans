namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractors", "CountryCode", c => c.Int());
            CreateIndex("dbo.Contractors", "CountryCode");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contractors", new[] { "CountryCode" });
            DropColumn("dbo.Contractors", "CountryCode");
        }
    }
}
