namespace ProiectPentalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adaugare : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "Name", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "Name", c => c.String());
        }
    }
}
