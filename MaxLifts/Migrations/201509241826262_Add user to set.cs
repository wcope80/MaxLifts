namespace MaxLifts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addusertoset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sets", "User", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sets", "User");
        }
    }
}
