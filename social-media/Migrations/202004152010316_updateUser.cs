namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Cover", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Cover");
        }
    }
}
