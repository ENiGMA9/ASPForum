namespace ASPForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuthorId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Threads", new[] { "Author_Id" });
            DropIndex("dbo.Replies", new[] { "Author_Id" });
            DropColumn("dbo.Threads", "AuthorId");
            DropColumn("dbo.Replies", "AuthorId");
            RenameColumn(table: "dbo.Threads", name: "Author_Id", newName: "AuthorId");
            RenameColumn(table: "dbo.Replies", name: "Author_Id", newName: "AuthorId");
            AlterColumn("dbo.Threads", "AuthorId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Replies", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Threads", "AuthorId");
            CreateIndex("dbo.Replies", "AuthorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Replies", new[] { "AuthorId" });
            DropIndex("dbo.Threads", new[] { "AuthorId" });
            AlterColumn("dbo.Replies", "AuthorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Threads", "AuthorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Replies", name: "AuthorId", newName: "Author_Id");
            RenameColumn(table: "dbo.Threads", name: "AuthorId", newName: "Author_Id");
            AddColumn("dbo.Replies", "AuthorId", c => c.Int(nullable: false));
            AddColumn("dbo.Threads", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Replies", "Author_Id");
            CreateIndex("dbo.Threads", "Author_Id");
        }
    }
}
