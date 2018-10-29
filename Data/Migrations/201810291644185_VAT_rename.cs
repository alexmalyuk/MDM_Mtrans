namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VAT_rename : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Contractors", new[] { "VATCertificateNumber" });
            RenameColumn("dbo.Contractors", "VATCertificateNumber", "VATNumber");
            CreateIndex("dbo.Contractors", "VATNumber");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contractors", new[] { "VATNumber" });
            RenameColumn("dbo.Contractors", "VATNumber", "VATCertificateNumber");
            CreateIndex("dbo.Contractors", "VATCertificateNumber");
        }
    }
}
