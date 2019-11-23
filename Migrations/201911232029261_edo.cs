namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServicioIncidencias",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Incidencia_Id = c.Long(),
                        Servicio_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incidencias", t => t.Incidencia_Id)
                .ForeignKey("dbo.Servicios", t => t.Servicio_Id)
                .Index(t => t.Incidencia_Id)
                .Index(t => t.Servicio_Id);
            
            AddColumn("dbo.Incidencias", "FechaInicio", c => c.DateTime(nullable: false));
            AddColumn("dbo.Incidencias", "FechaFin", c => c.DateTime(nullable: false));
            AddColumn("dbo.Servicios", "FechaEntrada", c => c.DateTime(nullable: false));
            AddColumn("dbo.Usuarios", "Nombre", c => c.String());
            AddColumn("dbo.Usuarios", "Apellido", c => c.String());
            AddColumn("dbo.Usuarios", "Telefono", c => c.String());
            DropColumn("dbo.Servicios", "FechaEntrata");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Servicios", "FechaEntrata", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.ServicioIncidencias", "Servicio_Id", "dbo.Servicios");
            DropForeignKey("dbo.ServicioIncidencias", "Incidencia_Id", "dbo.Incidencias");
            DropIndex("dbo.ServicioIncidencias", new[] { "Servicio_Id" });
            DropIndex("dbo.ServicioIncidencias", new[] { "Incidencia_Id" });
            DropColumn("dbo.Usuarios", "Telefono");
            DropColumn("dbo.Usuarios", "Apellido");
            DropColumn("dbo.Usuarios", "Nombre");
            DropColumn("dbo.Servicios", "FechaEntrada");
            DropColumn("dbo.Incidencias", "FechaFin");
            DropColumn("dbo.Incidencias", "FechaInicio");
            DropTable("dbo.ServicioIncidencias");
        }
    }
}
