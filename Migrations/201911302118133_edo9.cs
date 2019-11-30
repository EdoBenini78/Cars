﻿namespace CARS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edo9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Incidencias", "FechaFin", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Incidencias", "FechaFin", c => c.DateTime(nullable: false));
        }
    }
}