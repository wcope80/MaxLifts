namespace MaxLifts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sets",
                c => new
                    {
                        SetId = c.Int(nullable: false, identity: true),
                        Exercise = c.String(),
                        Weight = c.Int(nullable: false),
                        Reps = c.Int(nullable: false),
                        CalculatedMax = c.Int(nullable: false),
                        SetDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SetId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sets");
        }
    }
}
