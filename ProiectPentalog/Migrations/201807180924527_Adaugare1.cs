namespace ProiectPentalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adaugare1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "Name", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "Name", c => c.String());
        }
    }
}
