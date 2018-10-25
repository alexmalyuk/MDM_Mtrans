namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLink : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Nodes");
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ContractorId = c.Guid(nullable: false),
                        NodeId = c.Guid(nullable: false),
                        NativeId = c.String(nullable: false, maxLength: 36),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contractors", t => t.ContractorId, cascadeDelete: true)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .Index(t => t.ContractorId)
                .Index(t => t.NodeId)
                .Index(t => t.NativeId);
            
            AlterColumn("dbo.Nodes", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Nodes", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Nodes", "Alias", c => c.String(nullable: false, maxLength: 10));
            AddPrimaryKey("dbo.Nodes", "Id");
            CreateIndex("dbo.Nodes", "Name", unique: true);
            CreateIndex("dbo.Nodes", "Alias", unique: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.Links", "ContractorId", "dbo.Contractors");
            DropIndex("dbo.Nodes", new[] { "Alias" });
            DropIndex("dbo.Nodes", new[] { "Name" });
            DropIndex("dbo.Links", new[] { "NativeId" });
            DropIndex("dbo.Links", new[] { "NodeId" });
            DropIndex("dbo.Links", new[] { "ContractorId" });
            DropPrimaryKey("dbo.Nodes");
            AlterColumn("dbo.Nodes", "Alias", c => c.String(maxLength: 10));
            AlterColumn("dbo.Nodes", "Name", c => c.String());
            AlterColumn("dbo.Nodes", "Id", c => c.Guid(nullable: false));
            DropTable("dbo.Links");
            AddPrimaryKey("dbo.Nodes", "Id");
        }
    }
}
