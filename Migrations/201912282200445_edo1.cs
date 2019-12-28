namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edo1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Servicios", "Descripcion", c => c.String());
            AddColumn("dbo.Servicios", "NumeroOrden", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Servicios", "NumeroOrden");
            DropColumn("dbo.Servicios", "Descripcion");
        }
    }
}
