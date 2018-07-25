namespace ProiectPentalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adugare3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "Subject", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "Subject", c => c.String(maxLength: 20));
        }
    }
}
