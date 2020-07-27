namespace Lessons_Cordinator_v1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MIG2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "whatsAppNumber", c => c.String());
            DropColumn("dbo.Students", "age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "age", c => c.Int(nullable: false));
            DropColumn("dbo.Students", "whatsAppNumber");
        }
    }
}
