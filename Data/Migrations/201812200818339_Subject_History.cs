namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subject_History : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DateUTC = c.DateTime(nullable: false),
                        User = c.String(maxLength: 50),
                        SubjectXML = c.String(storeType: "xml"),
                        Node_Id = c.Guid(),
                        Subject_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.Node_Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.Node_Id)
                .Index(t => t.Subject_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Histories", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Histories", "Node_Id", "dbo.Nodes");
            DropIndex("dbo.Histories", new[] { "Subject_Id" });
            DropIndex("dbo.Histories", new[] { "Node_Id" });
            DropTable("dbo.Histories");
        }
    }
}
