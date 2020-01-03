namespace ASPForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Description", c => c.String());
            DropColumn("dbo.AspNetUsers", "LastSeen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "LastSeen", c => c.String());
            DropColumn("dbo.AspNetUsers", "Description");
        }
    }
}
