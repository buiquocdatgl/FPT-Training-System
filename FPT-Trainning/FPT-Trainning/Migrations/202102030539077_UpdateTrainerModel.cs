namespace FPT_Trainning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTrainerModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainers", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainers", "Phone", c => c.Int());
        }
    }
}
