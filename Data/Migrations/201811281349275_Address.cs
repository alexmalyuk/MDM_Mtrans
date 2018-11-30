namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractorAddreses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PostalCode = c.String(maxLength: 15),
                        Country = c.String(maxLength: 120),
                        Region = c.String(maxLength: 120),
                        District = c.String(maxLength: 120),
                        City = c.String(maxLength: 120),
                        Street = c.String(maxLength: 120),
                        House = c.String(maxLength: 20),
                        Flat = c.String(maxLength: 20),
                        StringRepresentedAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contractors", t => t.Id)
                .Index(t => t.Id);

            //AddColumn("dbo.Contractors", "CountryOfRegistration", c => c.Int());
            //DropColumn("dbo.Contractors", "CountryCode");
            RenameColumn("dbo.Contractors", "CountryCode", "CountryOfRegistration");
            AlterColumn("dbo.Contractors", "CountryOfRegistration", c => c.Int(nullable: true));

            DropColumn("dbo.Contractors", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contractors", "Country", c => c.Int(nullable: false));
            //AddColumn("dbo.Contractors", "CountryCode", c => c.Int(nullable: false));
            DropForeignKey("dbo.ContractorAddreses", "Id", "dbo.Contractors");
            DropIndex("dbo.ContractorAddreses", new[] { "Id" });
            //DropColumn("dbo.Contractors", "CountryOfRegistration");
            DropTable("dbo.ContractorAddreses");

            RenameColumn("dbo.Contractors", "CountryOfRegistration", "CountryCode");
            AlterColumn("dbo.Contractors", "CountryCode", c => c.Int(nullable: false));
        }
    }
}
