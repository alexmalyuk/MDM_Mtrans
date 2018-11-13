namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Country_And_TPC_Mapping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Links", "ContractorId", "dbo.Contractors");
            DropIndex("dbo.Contractors", new[] { "INN" });
            DropIndex("dbo.Links", new[] { "ContractorId" });
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Iso = c.Int(nullable: false),
                        Alpha2 = c.String(nullable: false, maxLength: 2),
                        Alpha3 = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Iso, unique: true)
                .Index(t => t.Alpha2, unique: true)
                .Index(t => t.Alpha3, unique: true);
            
            AddColumn("dbo.Links", "SubjectId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Links", "SubjectId");
            CreateIndex("dbo.Contractors", "INN");
            DropColumn("dbo.Links", "ContractorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Links", new[] { "SubjectId" });
            //AddColumn("dbo.Links", "ContractorId", c => c.Guid(nullable: false));
            //DropColumn("dbo.Links", "SubjectId");
            RenameColumn("dbo.Links", "SubjectId", "ContractorId");
            CreateIndex("dbo.Links", "ContractorId");

            DropIndex("dbo.Countries", new[] { "Alpha3" });
            DropIndex("dbo.Countries", new[] { "Alpha2" });
            DropIndex("dbo.Countries", new[] { "Iso" });
            DropIndex("dbo.Countries", new[] { "Name" });
            DropIndex("dbo.Contractors", new[] { "INN" });
            DropTable("dbo.Countries");
            CreateIndex("dbo.Contractors", "INN", unique: true);
            //AddForeignKey("dbo.Links", "ContractorId", "dbo.Contractors", "Id", cascadeDelete: false);
        }
    }
}
