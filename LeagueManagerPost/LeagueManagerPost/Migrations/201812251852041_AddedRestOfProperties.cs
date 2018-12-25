namespace LeagueManagerPost.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRestOfProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Games", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Games", "Location", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Teams", "Coach", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Teams", "Wins", c => c.Int(nullable: false));
            AddColumn("dbo.Teams", "Losses", c => c.Int(nullable: false));
            AlterColumn("dbo.Teams", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teams", "Name", c => c.String());
            DropColumn("dbo.Teams", "Losses");
            DropColumn("dbo.Teams", "Wins");
            DropColumn("dbo.Teams", "Coach");
            DropColumn("dbo.Games", "Location");
            DropColumn("dbo.Games", "Time");
            DropColumn("dbo.Games", "Date");
        }
    }
}
