namespace ASPForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Subjects", new[] { "Category_Id" });
            AlterColumn("dbo.Subjects", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Subjects", "Category_Id");
            AddForeignKey("dbo.Subjects", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Subjects", new[] { "Category_Id" });
            AlterColumn("dbo.Subjects", "Category_Id", c => c.Int());
            CreateIndex("dbo.Subjects", "Category_Id");
            AddForeignKey("dbo.Subjects", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
