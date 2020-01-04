namespace ASPForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRequired2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Threads", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Threads", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Replies", "Thread_Id", "dbo.Threads");
            DropIndex("dbo.Threads", new[] { "Author_Id" });
            DropIndex("dbo.Threads", new[] { "Subject_Id" });
            DropIndex("dbo.Replies", new[] { "Thread_Id" });
            AlterColumn("dbo.Threads", "Author_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Threads", "Subject_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Replies", "Thread_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Threads", "Author_Id");
            CreateIndex("dbo.Threads", "Subject_Id");
            CreateIndex("dbo.Replies", "Thread_Id");
            AddForeignKey("dbo.Threads", "Subject_Id", "dbo.Subjects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Threads", "Author_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Replies", "Thread_Id", "dbo.Threads", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replies", "Thread_Id", "dbo.Threads");
            DropForeignKey("dbo.Threads", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Threads", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.Replies", new[] { "Thread_Id" });
            DropIndex("dbo.Threads", new[] { "Subject_Id" });
            DropIndex("dbo.Threads", new[] { "Author_Id" });
            AlterColumn("dbo.Replies", "Thread_Id", c => c.Int());
            AlterColumn("dbo.Threads", "Subject_Id", c => c.Int());
            AlterColumn("dbo.Threads", "Author_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Replies", "Thread_Id");
            CreateIndex("dbo.Threads", "Subject_Id");
            CreateIndex("dbo.Threads", "Author_Id");
            AddForeignKey("dbo.Replies", "Thread_Id", "dbo.Threads", "Id");
            AddForeignKey("dbo.Threads", "Author_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Threads", "Subject_Id", "dbo.Subjects", "Id");
        }
    }
}
