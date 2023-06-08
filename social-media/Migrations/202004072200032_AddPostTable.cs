namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false, maxLength: 255),
                        DateTime = c.DateTime(),
                        UserID = c.Byte(nullable: false),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.User_UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "User_UserID", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "User_UserID" });
            DropTable("dbo.Posts");
        }
    }
}
