namespace ProiectPentalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddotari : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Dotari", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "Dotari");
        }
    }
}
