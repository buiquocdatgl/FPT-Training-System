namespace FPT_Trainning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseAndCourseId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainees", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trainees", "CourseId");
            AddForeignKey("dbo.Trainees", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "CourseId", "dbo.Courses");
            DropIndex("dbo.Trainees", new[] { "CourseId" });
            DropColumn("dbo.Trainees", "CourseId");
        }
    }
}
