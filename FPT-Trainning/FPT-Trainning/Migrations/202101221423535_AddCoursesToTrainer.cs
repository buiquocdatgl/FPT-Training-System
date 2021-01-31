namespace FPT_Trainning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCoursesToTrainer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainers", "course_Id", c => c.Int());
            CreateIndex("dbo.Trainers", "course_Id");
            AddForeignKey("dbo.Trainers", "course_Id", "dbo.Courses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainers", "course_Id", "dbo.Courses");
            DropIndex("dbo.Trainers", new[] { "course_Id" });
            DropColumn("dbo.Trainers", "course_Id");
        }
    }
}
