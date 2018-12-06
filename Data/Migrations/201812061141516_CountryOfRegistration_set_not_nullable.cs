namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryOfRegistration_set_not_nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contractors", "CountryOfRegistration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contractors", "CountryOfRegistration", c => c.Int());
        }
    }
}
