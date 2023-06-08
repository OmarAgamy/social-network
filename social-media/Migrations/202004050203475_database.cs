namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupUsers", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.GroupUsers", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Comments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Posts", "UserID", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropIndex("dbo.Posts", new[] { "UserID" });
            DropIndex("dbo.GroupUsers", new[] { "GroupID" });
            DropIndex("dbo.GroupUsers", new[] { "UserID" });
            RenameColumn(table: "dbo.Comments", name: "PostID", newName: "Post_PostID");
            RenameColumn(table: "dbo.Comments", name: "UserID", newName: "User_UserID");
            RenameColumn(table: "dbo.Posts", name: "UserID", newName: "User_UserID");
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        User_UserID = c.Int(nullable: false),
                        Group_GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserID, t.Group_GroupID })
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_GroupID, cascadeDelete: true)
                .Index(t => t.User_UserID)
                .Index(t => t.Group_GroupID);
            
            AddColumn("dbo.Users", "Password", c => c.String());
            AddColumn("dbo.Users", "User_UserID", c => c.Int());
            AlterColumn("dbo.Comments", "Post_PostID", c => c.Int());
            AlterColumn("dbo.Comments", "User_UserID", c => c.Int());
            AlterColumn("dbo.Posts", "User_UserID", c => c.Int());
            CreateIndex("dbo.Comments", "Post_PostID");
            CreateIndex("dbo.Comments", "User_UserID");
            CreateIndex("dbo.Posts", "User_UserID");
            CreateIndex("dbo.Users", "User_UserID");
            AddForeignKey("dbo.Users", "User_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Comments", "Post_PostID", "dbo.Posts", "PostID");
            AddForeignKey("dbo.Comments", "User_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Posts", "User_UserID", "dbo.Users", "UserID");
            DropTable("dbo.GroupUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GroupUsers",
                c => new
                    {
                        GUid = c.Int(nullable: false, identity: true),
                        GroupID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        AdminState = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GUid);
            
            DropForeignKey("dbo.Posts", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "Post_PostID", "dbo.Posts");
            DropForeignKey("dbo.UserGroups", "Group_GroupID", "dbo.Groups");
            DropForeignKey("dbo.UserGroups", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "User_UserID", "dbo.Users");
            DropIndex("dbo.UserGroups", new[] { "Group_GroupID" });
            DropIndex("dbo.UserGroups", new[] { "User_UserID" });
            DropIndex("dbo.Users", new[] { "User_UserID" });
            DropIndex("dbo.Posts", new[] { "User_UserID" });
            DropIndex("dbo.Comments", new[] { "User_UserID" });
            DropIndex("dbo.Comments", new[] { "Post_PostID" });
            AlterColumn("dbo.Posts", "User_UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "User_UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "Post_PostID", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "User_UserID");
            DropColumn("dbo.Users", "Password");
            DropTable("dbo.UserGroups");
            RenameColumn(table: "dbo.Posts", name: "User_UserID", newName: "UserID");
            RenameColumn(table: "dbo.Comments", name: "User_UserID", newName: "UserID");
            RenameColumn(table: "dbo.Comments", name: "Post_PostID", newName: "PostID");
            CreateIndex("dbo.GroupUsers", "UserID");
            CreateIndex("dbo.GroupUsers", "GroupID");
            CreateIndex("dbo.Posts", "UserID");
            CreateIndex("dbo.Comments", "UserID");
            CreateIndex("dbo.Comments", "PostID");
            AddForeignKey("dbo.Posts", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "PostID", "dbo.Posts", "PostID", cascadeDelete: true);
            AddForeignKey("dbo.GroupUsers", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.GroupUsers", "GroupID", "dbo.Groups", "GroupID", cascadeDelete: true);
        }
    }
}
