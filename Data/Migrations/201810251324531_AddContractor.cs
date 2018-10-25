namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContractor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        FullName = c.String(),
                        INN = c.String(nullable: false, maxLength: 12),
                        OKPO = c.String(maxLength: 10),
                        VATCertificateNumber = c.String(maxLength: 10),
                        LegalAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Nodes", "Alias", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Nodes", "Alias", c => c.String());
            DropTable("dbo.Contractors");
        }
    }
}
