namespace QueueApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsListening", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsListening");
        }
    }
}
