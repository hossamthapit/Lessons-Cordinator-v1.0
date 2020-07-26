namespace Lessons_Cordinator_v1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MIG1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        hour = c.Int(nullable: false),
                        minutes = c.Int(nullable: false),
                        day = c.Int(nullable: false),
                        gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        phone1 = c.String(),
                        phone2 = c.String(),
                        age = c.Int(nullable: false),
                        school = c.String(),
                        address = c.String(),
                        gender = c.Int(nullable: false),
                        group_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.group_ID)
                .Index(t => t.group_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "group_ID", "dbo.Groups");
            DropIndex("dbo.Students", new[] { "group_ID" });
            DropTable("dbo.Students");
            DropTable("dbo.Groups");
        }
    }
}
