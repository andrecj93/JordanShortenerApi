namespace UrlShortenerApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShortLink",
                c => new
                    {
                        Token = c.String(nullable: false, maxLength: 50),
                        FullLink = c.String(),
                        ShortenedLink = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        Clicks = c.Int(nullable: false),
                        LastClickDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        CreatedByIp = c.String(),
                        CreatedByUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Token)
                .ForeignKey("dbo.User", t => t.CreatedByUserId, cascadeDelete: true)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Email = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShortLink", "CreatedByUserId", "dbo.User");
            DropIndex("dbo.ShortLink", new[] { "CreatedByUserId" });
            DropTable("dbo.User");
            DropTable("dbo.ShortLink");
        }
    }
}
