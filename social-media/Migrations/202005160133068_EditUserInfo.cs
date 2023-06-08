namespace Social_Media.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditUserInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Education", c => c.String());
            AddColumn("dbo.Users", "Location", c => c.String());
            AddColumn("dbo.Users", "Birthdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Birthdate");
            DropColumn("dbo.Users", "Location");
            DropColumn("dbo.Users", "Education");
        }
    }
}
