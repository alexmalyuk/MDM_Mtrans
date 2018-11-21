namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeOfSubject = c.Int(nullable: false),
                        NativeId = c.String(),
                        Node_Id = c.Guid(nullable: false),
                        Subject_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.Node_Id, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .Index(t => t.Node_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Alias = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Alias, unique: true);
            
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(),
                        INN = c.String(nullable: false, maxLength: 12),
                        OKPO = c.String(maxLength: 10),
                        VATNumber = c.String(maxLength: 10),
                        LegalAddress = c.String(),
                        CountryCode = c.Int(nullable: false),
                        Country = c.Int(nullable: false),
                        TypeOfCounterparty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.INN)
                .Index(t => t.OKPO)
                .Index(t => t.VATNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contractors", "Id", "dbo.Subjects");
            DropForeignKey("dbo.Links", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Links", "Node_Id", "dbo.Nodes");
            DropIndex("dbo.Contractors", new[] { "VATNumber" });
            DropIndex("dbo.Contractors", new[] { "OKPO" });
            DropIndex("dbo.Contractors", new[] { "INN" });
            DropIndex("dbo.Contractors", new[] { "Id" });
            DropIndex("dbo.Nodes", new[] { "Alias" });
            DropIndex("dbo.Nodes", new[] { "Name" });
            DropIndex("dbo.Links", new[] { "Subject_Id" });
            DropIndex("dbo.Links", new[] { "Node_Id" });
            DropIndex("dbo.Subjects", new[] { "Name" });
            DropTable("dbo.Contractors");
            DropTable("dbo.Nodes");
            DropTable("dbo.Links");
            DropTable("dbo.Subjects");
        }
    }
}
