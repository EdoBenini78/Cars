namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edo2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehiculoChofers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Chofer_Id = c.Long(),
                        Vehiculo_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.Chofer_Id)
                .ForeignKey("dbo.Vehiculoes", t => t.Vehiculo_Id)
                .Index(t => t.Chofer_Id)
                .Index(t => t.Vehiculo_Id);
            
            AddColumn("dbo.Incidencias", "Vehiculo_Id", c => c.Long());
            CreateIndex("dbo.Incidencias", "Vehiculo_Id");
            AddForeignKey("dbo.Incidencias", "Vehiculo_Id", "dbo.Vehiculoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehiculoChofers", "Vehiculo_Id", "dbo.Vehiculoes");
            DropForeignKey("dbo.VehiculoChofers", "Chofer_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Incidencias", "Vehiculo_Id", "dbo.Vehiculoes");
            DropIndex("dbo.VehiculoChofers", new[] { "Vehiculo_Id" });
            DropIndex("dbo.VehiculoChofers", new[] { "Chofer_Id" });
            DropIndex("dbo.Incidencias", new[] { "Vehiculo_Id" });
            DropColumn("dbo.Incidencias", "Vehiculo_Id");
            DropTable("dbo.VehiculoChofers");
        }
    }
}
