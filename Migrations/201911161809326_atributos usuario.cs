namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atributosusuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Servicios", "FechaEntrada", c => c.DateTime(nullable: false));
            AddColumn("dbo.Usuarios", "Nombre", c => c.String());
            AddColumn("dbo.Usuarios", "Apellido", c => c.String());
            AddColumn("dbo.Usuarios", "Telefono", c => c.String());
            DropColumn("dbo.Servicios", "FechaEntrata");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Servicios", "FechaEntrata", c => c.DateTime(nullable: false));
            DropColumn("dbo.Usuarios", "Telefono");
            DropColumn("dbo.Usuarios", "Apellido");
            DropColumn("dbo.Usuarios", "Nombre");
            DropColumn("dbo.Servicios", "FechaEntrada");
        }
    }
}
