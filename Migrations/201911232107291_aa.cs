namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Incidencias", "FechaSugerida", c => c.DateTime(nullable: false));
            AddColumn("dbo.Incidencias", "Descripcion", c => c.String());
            AddColumn("dbo.Incidencias", "DireccionSugerida", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Incidencias", "DireccionSugerida");
            DropColumn("dbo.Incidencias", "Descripcion");
            DropColumn("dbo.Incidencias", "FechaSugerida");
        }
    }
}
