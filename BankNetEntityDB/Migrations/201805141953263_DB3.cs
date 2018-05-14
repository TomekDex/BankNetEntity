namespace BankNetEntityDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Pass = c.String(),
                        Login = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        Title = c.String(),
                        UserIDTo = c.Int(nullable: false),
                        UserIDFrom = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserIDFrom)
                .ForeignKey("dbo.Users", t => t.UserIDTo)
                .Index(t => t.UserIDTo)
                .Index(t => t.UserIDFrom);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transfers", "UserIDTo", "dbo.Users");
            DropForeignKey("dbo.Transfers", "UserIDFrom", "dbo.Users");
            DropIndex("dbo.Transfers", new[] { "UserIDFrom" });
            DropIndex("dbo.Transfers", new[] { "UserIDTo" });
            DropTable("dbo.Transfers");
            DropTable("dbo.Users");
        }
    }
}
