namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edo8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Incidencias", "FechaFin", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Incidencias", "FechaFin", c => c.DateTime(nullable: true));
        }
    }
}
