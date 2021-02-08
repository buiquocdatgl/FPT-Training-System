namespace FPT_Trainning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNotnullFieldTrainee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainees", "Age", c => c.Int());
            AlterColumn("dbo.Trainees", "Experience", c => c.Int());
            AlterColumn("dbo.Trainees", "ToeicScore", c => c.Int());
            AlterColumn("dbo.Trainers", "Phone", c => c.Int());
            AlterColumn("dbo.Trainers", "Type", c => c.Int());
            DropColumn("dbo.Trainees", "UserName");
            DropColumn("dbo.Trainers", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainers", "UserName", c => c.String());
            AddColumn("dbo.Trainees", "UserName", c => c.String());
            AlterColumn("dbo.Trainers", "Type", c => c.Int(nullable: false));
            AlterColumn("dbo.Trainers", "Phone", c => c.Int(nullable: false));
            AlterColumn("dbo.Trainees", "ToeicScore", c => c.Int(nullable: false));
            AlterColumn("dbo.Trainees", "Experience", c => c.Int(nullable: false));
            AlterColumn("dbo.Trainees", "Age", c => c.Int(nullable: false));
        }
    }
}
