namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupPosts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Group_GroupID", c => c.Int());
            CreateIndex("dbo.Posts", "Group_GroupID");
            AddForeignKey("dbo.Posts", "Group_GroupID", "dbo.Groups", "GroupID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Group_GroupID", "dbo.Groups");
            DropIndex("dbo.Posts", new[] { "Group_GroupID" });
            DropColumn("dbo.Posts", "Group_GroupID");
        }
    }
}
