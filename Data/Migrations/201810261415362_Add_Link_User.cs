namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Link_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Links", "User", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Links", "User");
        }
    }
}
