namespace ASPForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Threads", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Replies", "ThreadId", "dbo.Threads");
            DropIndex("dbo.Subjects", new[] { "CategoryId" });
            DropIndex("dbo.Threads", new[] { "SubjectId" });
            DropIndex("dbo.Replies", new[] { "ThreadId" });
            RenameColumn(table: "dbo.Subjects", name: "CategoryId", newName: "Category_Id");
            RenameColumn(table: "dbo.Threads", name: "SubjectId", newName: "Subject_Id");
            RenameColumn(table: "dbo.Threads", name: "AuthorId", newName: "Author_Id");
            RenameColumn(table: "dbo.Replies", name: "ThreadId", newName: "Thread_Id");
            RenameColumn(table: "dbo.Replies", name: "AuthorId", newName: "Author_Id");
            RenameIndex(table: "dbo.Threads", name: "IX_AuthorId", newName: "IX_Author_Id");
            RenameIndex(table: "dbo.Replies", name: "IX_AuthorId", newName: "IX_Author_Id");
            AlterColumn("dbo.Subjects", "Category_Id", c => c.Int());
            AlterColumn("dbo.Threads", "Subject_Id", c => c.Int());
            AlterColumn("dbo.Replies", "Thread_Id", c => c.Int());
            CreateIndex("dbo.Subjects", "Category_Id");
            CreateIndex("dbo.Threads", "Subject_Id");
            CreateIndex("dbo.Replies", "Thread_Id");
            AddForeignKey("dbo.Subjects", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.Threads", "Subject_Id", "dbo.Subjects", "Id");
            AddForeignKey("dbo.Replies", "Thread_Id", "dbo.Threads", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replies", "Thread_Id", "dbo.Threads");
            DropForeignKey("dbo.Threads", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Replies", new[] { "Thread_Id" });
            DropIndex("dbo.Threads", new[] { "Subject_Id" });
            DropIndex("dbo.Subjects", new[] { "Category_Id" });
            AlterColumn("dbo.Replies", "Thread_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Threads", "Subject_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Subjects", "Category_Id", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Replies", name: "IX_Author_Id", newName: "IX_AuthorId");
            RenameIndex(table: "dbo.Threads", name: "IX_Author_Id", newName: "IX_AuthorId");
            RenameColumn(table: "dbo.Replies", name: "Author_Id", newName: "AuthorId");
            RenameColumn(table: "dbo.Replies", name: "Thread_Id", newName: "ThreadId");
            RenameColumn(table: "dbo.Threads", name: "Author_Id", newName: "AuthorId");
            RenameColumn(table: "dbo.Threads", name: "Subject_Id", newName: "SubjectId");
            RenameColumn(table: "dbo.Subjects", name: "Category_Id", newName: "CategoryId");
            CreateIndex("dbo.Replies", "ThreadId");
            CreateIndex("dbo.Threads", "SubjectId");
            CreateIndex("dbo.Subjects", "CategoryId");
            AddForeignKey("dbo.Replies", "ThreadId", "dbo.Threads", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Threads", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Subjects", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
