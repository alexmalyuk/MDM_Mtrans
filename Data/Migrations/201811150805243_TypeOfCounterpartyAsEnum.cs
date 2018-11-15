namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeOfCounterpartyAsEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractors", "TypeOfCounterparty", c => c.Int(nullable: false));
            DropColumn("dbo.Contractors", "TypeOfCounterpartyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contractors", "TypeOfCounterpartyId", c => c.Int(nullable: false));
            DropColumn("dbo.Contractors", "TypeOfCounterparty");
        }
    }
}
