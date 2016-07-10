namespace Yourslides.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCreatedColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Presentation", "Created");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Presentation", "Created", c => c.Int(nullable: false));
        }
    }
}
