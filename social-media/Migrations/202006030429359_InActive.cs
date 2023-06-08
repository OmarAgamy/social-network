namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "InActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "InActive");
        }
    }
}
