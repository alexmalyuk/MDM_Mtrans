namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HeadBranch_Contractor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractors", "IsBranch", c => c.Boolean(nullable: false));
            AddColumn("dbo.Contractors", "HeadContractorId", c => c.Guid());
            AddColumn("dbo.Contractors", "BranchCode", c => c.String(maxLength: 20));
            CreateIndex("dbo.Contractors", "HeadContractorId");
            CreateIndex("dbo.Contractors", "BranchCode");
            AddForeignKey("dbo.Contractors", "HeadContractorId", "dbo.Contractors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contractors", "HeadContractorId", "dbo.Contractors");
            DropIndex("dbo.Contractors", new[] { "BranchCode" });
            DropIndex("dbo.Contractors", new[] { "HeadContractorId" });
            DropColumn("dbo.Contractors", "BranchCode");
            DropColumn("dbo.Contractors", "HeadContractorId");
            DropColumn("dbo.Contractors", "IsBranch");
        }
    }
}
