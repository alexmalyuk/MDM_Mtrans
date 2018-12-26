namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class History_SubjectTypeName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoryEntries", "SubjectTypeName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HistoryEntries", "SubjectTypeName");
        }
    }
}
