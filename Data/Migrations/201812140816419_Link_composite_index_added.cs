namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Link_composite_index_added : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Links", "Node_Id", "dbo.Nodes");
            RenameColumn(table: "dbo.Links", name: "Subject_Id", newName: "SubjectId");
            RenameColumn(table: "dbo.Links", name: "Node_Id", newName: "NodeId");
            RenameIndex(table: "dbo.Links", name: "IX_Subject_Id", newName: "IX_SubjectId");
            RenameIndex(table: "dbo.Links", name: "IX_Node_Id", newName: "IX_NodeId");
            DropPrimaryKey("dbo.Links");
            AlterColumn("dbo.Links", "NativeId", c => c.String(nullable: false, maxLength: 36));
            AddPrimaryKey("dbo.Links", new[] { "SubjectId", "NodeId" });
            AddForeignKey("dbo.Links", "NodeId", "dbo.Nodes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "NodeId", "dbo.Nodes");
            DropPrimaryKey("dbo.Links");
            AlterColumn("dbo.Links", "NativeId", c => c.String());
            AddPrimaryKey("dbo.Links", "Id");
            RenameIndex(table: "dbo.Links", name: "IX_NodeId", newName: "IX_Node_Id");
            RenameIndex(table: "dbo.Links", name: "IX_SubjectId", newName: "IX_Subject_Id");
            RenameColumn(table: "dbo.Links", name: "NodeId", newName: "Node_Id");
            RenameColumn(table: "dbo.Links", name: "SubjectId", newName: "Subject_Id");
            AddForeignKey("dbo.Links", "Node_Id", "dbo.Nodes", "Id", cascadeDelete: true);
        }
    }
}
