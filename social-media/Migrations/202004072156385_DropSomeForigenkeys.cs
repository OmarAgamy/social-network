namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropSomeForigenkeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Post_PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Group_GroupID", "dbo.Groups");
            DropForeignKey("dbo.Posts", "User_UserID", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "Post_PostID" });
            DropIndex("dbo.Posts", new[] { "Group_GroupID" });
            DropIndex("dbo.Posts", new[] { "User_UserID" });
            DropColumn("dbo.Comments", "Post_PostID");
            DropColumn("dbo.Posts", "Group_GroupID");
            DropColumn("dbo.Posts", "User_UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "User_UserID", c => c.Int());
            AddColumn("dbo.Posts", "Group_GroupID", c => c.Int());
            AddColumn("dbo.Comments", "Post_PostID", c => c.Int());
            CreateIndex("dbo.Posts", "User_UserID");
            CreateIndex("dbo.Posts", "Group_GroupID");
            CreateIndex("dbo.Comments", "Post_PostID");
            AddForeignKey("dbo.Posts", "User_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Posts", "Group_GroupID", "dbo.Groups", "GroupID");
            AddForeignKey("dbo.Comments", "Post_PostID", "dbo.Posts", "PostID");
        }
    }
}
