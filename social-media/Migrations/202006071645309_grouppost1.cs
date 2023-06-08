namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class grouppost1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GroupUsers", newName: "UserGroups");
            RenameColumn(table: "dbo.Posts", name: "Group_GroupID", newName: "GroupID");
            RenameIndex(table: "dbo.Posts", name: "IX_Group_GroupID", newName: "IX_GroupID");
            DropPrimaryKey("dbo.UserGroups");
            AddPrimaryKey("dbo.UserGroups", new[] { "User_UserID", "Group_GroupID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserGroups");
            AddPrimaryKey("dbo.UserGroups", new[] { "Group_GroupID", "User_UserID" });
            RenameIndex(table: "dbo.Posts", name: "IX_GroupID", newName: "IX_Group_GroupID");
            RenameColumn(table: "dbo.Posts", name: "GroupID", newName: "Group_GroupID");
            RenameTable(name: "dbo.UserGroups", newName: "GroupUsers");
        }
    }
}
