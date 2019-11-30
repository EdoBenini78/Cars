namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edo6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Servicios", "Incidencia_Id", "dbo.Incidencias");
            DropIndex("dbo.Servicios", new[] { "Incidencia_Id" });
            DropColumn("dbo.Servicios", "Incidencia_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Servicios", "Incidencia_Id", c => c.Long());
            CreateIndex("dbo.Servicios", "Incidencia_Id");
            AddForeignKey("dbo.Servicios", "Incidencia_Id", "dbo.Incidencias", "Id");
        }
    }
}
