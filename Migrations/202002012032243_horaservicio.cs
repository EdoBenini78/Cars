namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class horaservicio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Servicios", "Hora", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Servicios", "Hora");
        }
    }
}
