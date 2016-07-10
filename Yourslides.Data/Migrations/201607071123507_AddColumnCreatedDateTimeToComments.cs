namespace Yourslides.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnCreatedDateTimeToComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CreatedDateTime", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "CreatedDateTime");
        }
    }
}
