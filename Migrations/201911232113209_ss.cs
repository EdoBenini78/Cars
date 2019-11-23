namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Incidencias", "Kilometraje", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Incidencias", "Kilometraje");
        }
    }
}
