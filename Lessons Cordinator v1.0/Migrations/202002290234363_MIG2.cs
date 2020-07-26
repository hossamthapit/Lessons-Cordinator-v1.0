namespace Lessons_Cordinator_v1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MIG2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "group_ID", "dbo.Groups");
            DropIndex("dbo.Students", new[] { "group_ID" });
            AddColumn("dbo.Students", "groupID", c => c.Int(nullable: false));
            DropColumn("dbo.Students", "group_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "group_ID", c => c.Int());
            DropColumn("dbo.Students", "groupID");
            CreateIndex("dbo.Students", "group_ID");
            AddForeignKey("dbo.Students", "group_ID", "dbo.Groups", "ID");
        }
    }
}
