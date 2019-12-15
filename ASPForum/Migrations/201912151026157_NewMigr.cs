namespace ASPForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Threads", "Content", c => c.String());
            AddColumn("dbo.Threads", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Avatar", c => c.String());
            AddColumn("dbo.AspNetUsers", "Location", c => c.String());
            AddColumn("dbo.AspNetUsers", "JoinDate", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastSeen", c => c.String());
            AddColumn("dbo.Replies", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Replies", "CreatedAt");
            DropColumn("dbo.AspNetUsers", "LastSeen");
            DropColumn("dbo.AspNetUsers", "JoinDate");
            DropColumn("dbo.AspNetUsers", "Location");
            DropColumn("dbo.AspNetUsers", "Avatar");
            DropColumn("dbo.Threads", "CreatedAt");
            DropColumn("dbo.Threads", "Content");
        }
    }
}
