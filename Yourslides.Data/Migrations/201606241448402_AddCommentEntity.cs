namespace Yourslides.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PresentationId = c.Long(nullable: false),
                        OwnerId = c.String(nullable: false, maxLength: 128),
                        Text = c.String(nullable: false, maxLength: 500),
                        Created = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .ForeignKey("dbo.Presentation", t => t.PresentationId, cascadeDelete: true)
                .Index(t => t.PresentationId)
                .Index(t => t.OwnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PresentationId", "dbo.Presentation");
            DropForeignKey("dbo.Comments", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "OwnerId" });
            DropIndex("dbo.Comments", new[] { "PresentationId" });
            DropTable("dbo.Comments");
        }
    }
}
