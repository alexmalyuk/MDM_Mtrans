namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class set_lengths_20_for_ContractorCodes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Contractors", new[] { "INN" });
            DropIndex("dbo.Contractors", new[] { "OKPO" });
            DropIndex("dbo.Contractors", new[] { "VATNumber" });
            AlterColumn("dbo.Contractors", "INN", c => c.String(maxLength: 20));
            AlterColumn("dbo.Contractors", "OKPO", c => c.String(maxLength: 20));
            AlterColumn("dbo.Contractors", "VATNumber", c => c.String(maxLength: 20));
            CreateIndex("dbo.Contractors", "INN");
            CreateIndex("dbo.Contractors", "OKPO");
            CreateIndex("dbo.Contractors", "VATNumber");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contractors", new[] { "VATNumber" });
            DropIndex("dbo.Contractors", new[] { "OKPO" });
            DropIndex("dbo.Contractors", new[] { "INN" });
            AlterColumn("dbo.Contractors", "VATNumber", c => c.String(maxLength: 10));
            AlterColumn("dbo.Contractors", "OKPO", c => c.String(maxLength: 10));
            AlterColumn("dbo.Contractors", "INN", c => c.String(nullable: false, maxLength: 12));
            CreateIndex("dbo.Contractors", "VATNumber");
            CreateIndex("dbo.Contractors", "OKPO");
            CreateIndex("dbo.Contractors", "INN");
        }
    }
}
