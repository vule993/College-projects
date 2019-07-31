namespace QueueApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        Active = c.Boolean(nullable: false),
                        DestructivePower = c.Double(nullable: false),
                        Name = c.String(),
                        Quantity = c.Double(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        Z = c.Double(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.PositionId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Positions", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Items", "User_UserId", "dbo.Users");
            DropIndex("dbo.Positions", new[] { "User_UserId" });
            DropIndex("dbo.Items", new[] { "User_UserId" });
            DropTable("dbo.Positions");
            DropTable("dbo.Items");
        }
    }
}
