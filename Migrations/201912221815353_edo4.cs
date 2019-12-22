namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edo4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Servicios", "FechaEntrada", c => c.DateTime());
            AlterColumn("dbo.Servicios", "FechaSalida", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Servicios", "FechaSalida", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Servicios", "FechaEntrada", c => c.DateTime(nullable: false));
        }
    }
}
