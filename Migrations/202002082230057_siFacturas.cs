namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class siFacturas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Facturas", "Servicio_Id", c => c.Long());
            CreateIndex("dbo.Facturas", "Servicio_Id");
            AddForeignKey("dbo.Facturas", "Servicio_Id", "dbo.Servicios", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facturas", "Servicio_Id", "dbo.Servicios");
            DropIndex("dbo.Facturas", new[] { "Servicio_Id" });
            DropColumn("dbo.Facturas", "Servicio_Id");
        }
    }
}
