namespace Yourslides.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCreatedColumnFromComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PresentationWatches", "WatchDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Comments", "Created");
            DropColumn("dbo.PresentationWatches", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PresentationWatches", "DateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "Created", c => c.Int(nullable: false));
            DropColumn("dbo.PresentationWatches", "WatchDateTime");
        }
    }
}
