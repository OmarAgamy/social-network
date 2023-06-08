namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messenger : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Seen = c.Boolean(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Receiver_UserID = c.Int(),
                        Sender_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.Users", t => t.Receiver_UserID)
                .ForeignKey("dbo.Users", t => t.Sender_UserID)
                .Index(t => t.Receiver_UserID)
                .Index(t => t.Sender_UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Sender_UserID", "dbo.Users");
            DropForeignKey("dbo.Messages", "Receiver_UserID", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "Sender_UserID" });
            DropIndex("dbo.Messages", new[] { "Receiver_UserID" });
            DropTable("dbo.Messages");
        }
    }
}
