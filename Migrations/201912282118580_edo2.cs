namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tallers", "Longitud", c => c.Double(nullable: false));
            AddColumn("dbo.Tallers", "Latitud", c => c.Double(nullable: false));
            AlterColumn("dbo.Incidencias", "Longitud", c => c.Double(nullable: false));
            AlterColumn("dbo.Incidencias", "Latitud", c => c.Double(nullable: false));
            DropColumn("dbo.Tallers", "X");
            DropColumn("dbo.Tallers", "Y");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tallers", "Y", c => c.Long(nullable: false));
            AddColumn("dbo.Tallers", "X", c => c.Long(nullable: false));
            AlterColumn("dbo.Incidencias", "Latitud", c => c.Long(nullable: false));
            AlterColumn("dbo.Incidencias", "Longitud", c => c.Long(nullable: false));
            DropColumn("dbo.Tallers", "Latitud");
            DropColumn("dbo.Tallers", "Longitud");
        }
    }
}
