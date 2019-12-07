namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kari : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuarios", "Mail", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "Contrasenia", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "Nombre", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "Apellido", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "Telefono", c => c.String(nullable: false));
            AlterColumn("dbo.Vehiculoes", "Matricula", c => c.String(nullable: false));
            AlterColumn("dbo.Vehiculoes", "Marca", c => c.String(nullable: false));
            AlterColumn("dbo.Vehiculoes", "Modelo", c => c.String(nullable: false));
            AlterColumn("dbo.Vehiculoes", "Motor", c => c.String(nullable: false));
            AlterColumn("dbo.Vehiculoes", "Chasis", c => c.String(nullable: false));
            AlterColumn("dbo.Vehiculoes", "Combustible", c => c.String(nullable: false));
            AlterColumn("dbo.Tallers", "Nombre", c => c.String(nullable: false));
            AlterColumn("dbo.Tallers", "NombreContacto", c => c.String(nullable: false));
            AlterColumn("dbo.Tallers", "Direccion", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tallers", "Direccion", c => c.String());
            AlterColumn("dbo.Tallers", "NombreContacto", c => c.String());
            AlterColumn("dbo.Tallers", "Nombre", c => c.String());
            AlterColumn("dbo.Vehiculoes", "Combustible", c => c.String());
            AlterColumn("dbo.Vehiculoes", "Chasis", c => c.String());
            AlterColumn("dbo.Vehiculoes", "Motor", c => c.String());
            AlterColumn("dbo.Vehiculoes", "Modelo", c => c.String());
            AlterColumn("dbo.Vehiculoes", "Marca", c => c.String());
            AlterColumn("dbo.Vehiculoes", "Matricula", c => c.String());
            AlterColumn("dbo.Usuarios", "Telefono", c => c.String());
            AlterColumn("dbo.Usuarios", "Apellido", c => c.String());
            AlterColumn("dbo.Usuarios", "Nombre", c => c.String());
            AlterColumn("dbo.Usuarios", "Contrasenia", c => c.String());
            AlterColumn("dbo.Usuarios", "Mail", c => c.String());
        }
    }
}
