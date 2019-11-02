namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Monto = c.Double(nullable: false),
                        Numero = c.Long(nullable: false),
                        Servicio_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Servicios", t => t.Servicio_Id)
                .Index(t => t.Servicio_Id);
            
            CreateTable(
                "dbo.Incidencias",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Estado = c.Int(nullable: false),
                        Usuario_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Servicios",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Tipo = c.Int(nullable: false),
                        FechaSugerida = c.DateTime(nullable: false),
                        FechaEntrata = c.DateTime(nullable: false),
                        FechaSalida = c.DateTime(nullable: false),
                        Estado = c.Int(nullable: false),
                        Taller_Id = c.Long(),
                        Vehiculo_Id = c.Long(),
                        Incidencia_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tallers", t => t.Taller_Id)
                .ForeignKey("dbo.Vehiculoes", t => t.Vehiculo_Id)
                .ForeignKey("dbo.Incidencias", t => t.Incidencia_Id)
                .Index(t => t.Taller_Id)
                .Index(t => t.Vehiculo_Id)
                .Index(t => t.Incidencia_Id);
            
            CreateTable(
                "dbo.Tallers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nombre = c.String(),
                        Rut = c.Long(nullable: false),
                        NombreContacto = c.String(),
                        Telefono = c.Int(nullable: false),
                        Activo = c.Boolean(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        X = c.Long(nullable: false),
                        Y = c.Long(nullable: false),
                        Direccion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehiculoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Matricula = c.String(),
                        Kilometros = c.Double(nullable: false),
                        FechaDeCompra = c.DateTime(nullable: false),
                        numeroUnidad = c.Int(nullable: false),
                        Marca = c.String(),
                        Modelo = c.String(),
                        Anio = c.Int(nullable: false),
                        Motor = c.String(),
                        Chasis = c.String(),
                        Padron = c.Long(nullable: false),
                        Traccion = c.Int(nullable: false),
                        Combustible = c.String(),
                        Activo = c.Boolean(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        Localidad_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Localidads", t => t.Localidad_Id)
                .Index(t => t.Localidad_Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Mail = c.String(),
                        Contrasenia = c.String(),
                        Activo = c.Boolean(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        Tipo = c.Int(nullable: false),
                        Vehiculo_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehiculoes", t => t.Vehiculo_Id)
                .Index(t => t.Vehiculo_Id);
            
            CreateTable(
                "dbo.Localidads",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Departamento = c.String(),
                        Ciudad = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Incidencias", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Servicios", "Incidencia_Id", "dbo.Incidencias");
            DropForeignKey("dbo.Servicios", "Vehiculo_Id", "dbo.Vehiculoes");
            DropForeignKey("dbo.Vehiculoes", "Localidad_Id", "dbo.Localidads");
            DropForeignKey("dbo.Usuarios", "Vehiculo_Id", "dbo.Vehiculoes");
            DropForeignKey("dbo.Servicios", "Taller_Id", "dbo.Tallers");
            DropForeignKey("dbo.Facturas", "Servicio_Id", "dbo.Servicios");
            DropIndex("dbo.Usuarios", new[] { "Vehiculo_Id" });
            DropIndex("dbo.Vehiculoes", new[] { "Localidad_Id" });
            DropIndex("dbo.Servicios", new[] { "Incidencia_Id" });
            DropIndex("dbo.Servicios", new[] { "Vehiculo_Id" });
            DropIndex("dbo.Servicios", new[] { "Taller_Id" });
            DropIndex("dbo.Incidencias", new[] { "Usuario_Id" });
            DropIndex("dbo.Facturas", new[] { "Servicio_Id" });
            DropTable("dbo.Localidads");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Vehiculoes");
            DropTable("dbo.Tallers");
            DropTable("dbo.Servicios");
            DropTable("dbo.Incidencias");
            DropTable("dbo.Facturas");
        }
    }
}
