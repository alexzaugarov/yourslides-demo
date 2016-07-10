namespace Yourslides.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPresentationWatchEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PresentationWatches",
                c => new
                    {
                        PresentationId = c.Long(nullable: false),
                        VisitorId = c.String(nullable: false, maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PresentationId, t.VisitorId })
                .ForeignKey("dbo.Presentation", t => t.PresentationId, cascadeDelete: true)
                .Index(t => t.PresentationId);
            
            AddColumn("dbo.Presentation", "CreatedDateTime", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PresentationWatches", "PresentationId", "dbo.Presentation");
            DropIndex("dbo.PresentationWatches", new[] { "PresentationId" });
            DropColumn("dbo.Presentation", "CreatedDateTime");
            DropTable("dbo.PresentationWatches");
        }
    }
}
