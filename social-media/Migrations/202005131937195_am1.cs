namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class am1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "like_post", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "like_post");
        }
    }
}
