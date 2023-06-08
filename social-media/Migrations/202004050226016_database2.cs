namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "User_UserID", "dbo.Users");
            DropIndex("dbo.Users", new[] { "User_UserID" });
            CreateTable(
                "dbo.RelationShips",
                c => new
                    {
                        RelationID = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        ActionUser_UserID = c.Int(),
                        Friend_UserID = c.Int(),
                        Mate_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.RelationID)
                .ForeignKey("dbo.Users", t => t.ActionUser_UserID)
                .ForeignKey("dbo.Users", t => t.Friend_UserID)
                .ForeignKey("dbo.Users", t => t.Mate_UserID)
                .Index(t => t.ActionUser_UserID)
                .Index(t => t.Friend_UserID)
                .Index(t => t.Mate_UserID);
            
            DropColumn("dbo.Users", "User_UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "User_UserID", c => c.Int());
            DropForeignKey("dbo.RelationShips", "Mate_UserID", "dbo.Users");
            DropForeignKey("dbo.RelationShips", "Friend_UserID", "dbo.Users");
            DropForeignKey("dbo.RelationShips", "ActionUser_UserID", "dbo.Users");
            DropIndex("dbo.RelationShips", new[] { "Mate_UserID" });
            DropIndex("dbo.RelationShips", new[] { "Friend_UserID" });
            DropIndex("dbo.RelationShips", new[] { "ActionUser_UserID" });
            DropTable("dbo.RelationShips");
            CreateIndex("dbo.Users", "User_UserID");
            AddForeignKey("dbo.Users", "User_UserID", "dbo.Users", "UserID");
        }
    }
}
