namespace Yourslides.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPresentationEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Presentation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        OwnerId = c.String(nullable: false, maxLength: 128),
                        WatchCount = c.Long(nullable: false),
                        SlideCount = c.Int(nullable: false),
                        Created = c.Int(nullable: false),
                        LogoSlideIndex = c.Int(nullable: false),
                        Visibility = c.Int(nullable: false),
                        CommentEnable = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 1000),
                        Color = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Presentation", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Presentation", new[] { "OwnerId" });
            DropTable("dbo.Presentation");
        }
    }
}
