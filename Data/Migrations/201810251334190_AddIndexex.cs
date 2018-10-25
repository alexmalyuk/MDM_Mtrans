namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Contractors", "Name");
            CreateIndex("dbo.Contractors", "INN", unique: true);
            CreateIndex("dbo.Contractors", "OKPO");
            CreateIndex("dbo.Contractors", "VATCertificateNumber");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contractors", new[] { "VATCertificateNumber" });
            DropIndex("dbo.Contractors", new[] { "OKPO" });
            DropIndex("dbo.Contractors", new[] { "INN" });
            DropIndex("dbo.Contractors", new[] { "Name" });
        }
    }
}
