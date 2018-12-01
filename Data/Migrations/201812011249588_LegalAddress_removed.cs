namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LegalAddress_removed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contractors", "LegalAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contractors", "LegalAddress", c => c.String());
        }
    }
}
