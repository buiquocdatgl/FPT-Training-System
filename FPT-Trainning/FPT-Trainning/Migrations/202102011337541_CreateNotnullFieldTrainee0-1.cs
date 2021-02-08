namespace FPT_Trainning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNotnullFieldTrainee01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainees", "DOB", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainees", "DOB", c => c.DateTime(nullable: false));
        }
    }
}
