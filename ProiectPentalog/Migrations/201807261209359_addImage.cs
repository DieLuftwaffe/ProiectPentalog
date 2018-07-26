namespace ProiectPentalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "BrandImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "BrandImage");
        }
    }
}
