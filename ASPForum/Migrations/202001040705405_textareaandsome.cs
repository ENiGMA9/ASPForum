namespace ASPForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class textareaandsome : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Threads", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Threads", "Content", c => c.String());
        }
    }
}
