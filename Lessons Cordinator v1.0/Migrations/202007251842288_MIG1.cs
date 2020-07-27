namespace Lessons_Cordinator_v1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MIG1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.dayInformations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        studentID = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        absent = c.Boolean(nullable: false),
                        mark = c.Double(nullable: false),
                        note = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.dayInformations");
        }
    }
}
