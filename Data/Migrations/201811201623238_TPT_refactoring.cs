namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TPT_refactoring : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Links", "ContractorId", "dbo.Contractors");
            DropForeignKey("dbo.Links", "NodeId", "dbo.Nodes");
            DropIndex("dbo.Contractors", new[] { "Name" });
            DropIndex("dbo.Contractors", new[] { "INN" });
            DropIndex("dbo.Contractors", new[] { "CountryCode" });
            DropIndex("dbo.Links", new[] { "ContractorId" });
            DropIndex("dbo.Links", new[] { "NativeId" });
            RenameColumn(table: "dbo.Links", name: "NodeId", newName: "Node_Id");
            RenameIndex(table: "dbo.Links", name: "IX_NodeId", newName: "IX_Node_Id");
            DropPrimaryKey("dbo.Contractors");
            DropPrimaryKey("dbo.Links");
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name);
            
            AddColumn("dbo.Contractors", "Country", c => c.Int(nullable: false));
            AddColumn("dbo.Links", "TypeOfSubject", c => c.Int(nullable: false));
            AddColumn("dbo.Links", "Subject_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Contractors", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Contractors", "CountryCode", c => c.Int(nullable: false));
            AlterColumn("dbo.Links", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Links", "NativeId", c => c.String());
            AddPrimaryKey("dbo.Contractors", "Id");
            AddPrimaryKey("dbo.Links", "Id");
            CreateIndex("dbo.Links", "Subject_Id");
            CreateIndex("dbo.Contractors", "Id");
            CreateIndex("dbo.Contractors", "INN");
            AddForeignKey("dbo.Links", "Subject_Id", "dbo.Subjects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Contractors", "Id", "dbo.Subjects", "Id");
            AddForeignKey("dbo.Links", "Node_Id", "dbo.Nodes", "Id", cascadeDelete: true);
            DropColumn("dbo.Contractors", "Name");
            DropColumn("dbo.Links", "Date");
            DropColumn("dbo.Links", "ContractorId");
            DropColumn("dbo.Links", "User");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Links", "User", c => c.String(maxLength: 40));
            AddColumn("dbo.Links", "ContractorId", c => c.Guid(nullable: false));
            AddColumn("dbo.Links", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contractors", "Name", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.Links", "Node_Id", "dbo.Nodes");
            DropForeignKey("dbo.Contractors", "Id", "dbo.Subjects");
            DropForeignKey("dbo.Links", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.Contractors", new[] { "INN" });
            DropIndex("dbo.Contractors", new[] { "Id" });
            DropIndex("dbo.Links", new[] { "Subject_Id" });
            DropIndex("dbo.Subjects", new[] { "Name" });
            DropPrimaryKey("dbo.Links");
            DropPrimaryKey("dbo.Contractors");
            AlterColumn("dbo.Links", "NativeId", c => c.String(nullable: false, maxLength: 36));
            AlterColumn("dbo.Links", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Contractors", "CountryCode", c => c.Int());
            AlterColumn("dbo.Contractors", "Id", c => c.Guid(nullable: false, identity: true));
            DropColumn("dbo.Links", "Subject_Id");
            DropColumn("dbo.Links", "TypeOfSubject");
            DropColumn("dbo.Contractors", "Country");
            DropTable("dbo.Subjects");
            AddPrimaryKey("dbo.Links", "Id");
            AddPrimaryKey("dbo.Contractors", "Id");
            RenameIndex(table: "dbo.Links", name: "IX_Node_Id", newName: "IX_NodeId");
            RenameColumn(table: "dbo.Links", name: "Node_Id", newName: "NodeId");
            CreateIndex("dbo.Links", "NativeId");
            CreateIndex("dbo.Links", "ContractorId");
            CreateIndex("dbo.Contractors", "CountryCode");
            CreateIndex("dbo.Contractors", "INN", unique: true);
            CreateIndex("dbo.Contractors", "Name");
            AddForeignKey("dbo.Links", "NodeId", "dbo.Nodes", "Id");
            AddForeignKey("dbo.Links", "ContractorId", "dbo.Contractors", "Id", cascadeDelete: true);
        }
    }
}
