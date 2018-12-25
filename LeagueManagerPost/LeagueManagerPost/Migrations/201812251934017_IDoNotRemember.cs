namespace LeagueManagerPost.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IDoNotRemember : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.String(),
                        Start = c.DateTime(),
                        End = c.DateTime(),
                        ThemeColor = c.String(),
                        IsFullDay = c.Boolean(nullable: false),
                        Event = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Events");
        }
    }
}
