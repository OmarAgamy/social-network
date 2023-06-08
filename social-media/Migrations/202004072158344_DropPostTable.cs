namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropPostTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Posts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        Info = c.String(),
                        Privacy = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PostID);
            
        }
    }
}
