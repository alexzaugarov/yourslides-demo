namespace Yourslides.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusColumnToPresentationEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Presentation", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Presentation", "Status");
        }
    }
}
