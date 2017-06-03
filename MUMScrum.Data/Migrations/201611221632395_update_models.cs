namespace MUMScrum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_models : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkLogs", "WorkCompleted", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkLogs", "WorkCompleted", c => c.Int(nullable: false));
        }
    }
}
