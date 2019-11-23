namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EE : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuarios", "Vehiculo_Id", "dbo.Vehiculoes");
            DropIndex("dbo.Usuarios", new[] { "Vehiculo_Id" });
            DropColumn("dbo.Usuarios", "Vehiculo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "Vehiculo_Id", c => c.Long());
            CreateIndex("dbo.Usuarios", "Vehiculo_Id");
            AddForeignKey("dbo.Usuarios", "Vehiculo_Id", "dbo.Vehiculoes", "Id");
        }
    }
}
