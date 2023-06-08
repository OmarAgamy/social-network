namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comment : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserGroups", newName: "GroupUsers");
            DropPrimaryKey("dbo.GroupUsers");
            AddColumn("dbo.Comments", "UserID", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.GroupUsers", new[] { "Group_GroupID", "User_UserID" });
            DropColumn("dbo.Comments", "Like");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Like", c => c.Boolean(nullable: false));
            DropPrimaryKey("dbo.GroupUsers");
            DropColumn("dbo.Comments", "UserID");
            AddPrimaryKey("dbo.GroupUsers", new[] { "User_UserID", "Group_GroupID" });
            RenameTable(name: "dbo.GroupUsers", newName: "UserGroups");
        }
    }
}
