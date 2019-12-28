namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edo3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Incidencias", "Longitud", c => c.Long(nullable: false));
            AddColumn("dbo.Incidencias", "Latitud", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Incidencias", "Latitud");
            DropColumn("dbo.Incidencias", "Longitud");
        }
    }
}
