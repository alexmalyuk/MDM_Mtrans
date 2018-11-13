namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeOfCounterparty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractors", "TypeOfCounterpartyId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contractors", "TypeOfCounterpartyId");
        }
    }
}
