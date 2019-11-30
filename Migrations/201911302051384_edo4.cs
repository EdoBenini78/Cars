namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edo4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehiculoChofers", "Activo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VehiculoChofers", "Activo");
        }
    }
}
