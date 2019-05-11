namespace Caalinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class matchModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatchModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Horse1Id = c.Int(nullable: false),
                        Horse2Id = c.Int(nullable: false),
                        Like1 = c.Boolean(nullable: false),
                        Like2 = c.Boolean(nullable: false),
                        Match = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MatchModels", new[] { "ApplicationUser_Id" });
            DropTable("dbo.MatchModels");
        }
    }
}
