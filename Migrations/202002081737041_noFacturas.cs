namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noFacturas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Facturas", "Servicio_Id", "dbo.Servicios");
            DropIndex("dbo.Facturas", new[] { "Servicio_Id" });
            DropColumn("dbo.Facturas", "Servicio_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Facturas", "Servicio_Id", c => c.Long());
            CreateIndex("dbo.Facturas", "Servicio_Id");
            AddForeignKey("dbo.Facturas", "Servicio_Id", "dbo.Servicios", "Id");
        }
    }
}
