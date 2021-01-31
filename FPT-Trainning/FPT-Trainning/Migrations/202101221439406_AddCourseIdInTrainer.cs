namespace FPT_Trainning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseIdInTrainer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trainers", "course_Id", "dbo.Courses");
            DropIndex("dbo.Trainers", new[] { "course_Id" });
            RenameColumn(table: "dbo.Trainers", name: "course_Id", newName: "CourseId");
            AlterColumn("dbo.Trainers", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trainers", "CourseId");
            AddForeignKey("dbo.Trainers", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainers", "CourseId", "dbo.Courses");
            DropIndex("dbo.Trainers", new[] { "CourseId" });
            AlterColumn("dbo.Trainers", "CourseId", c => c.Int());
            RenameColumn(table: "dbo.Trainers", name: "CourseId", newName: "course_Id");
            CreateIndex("dbo.Trainers", "course_Id");
            AddForeignKey("dbo.Trainers", "course_Id", "dbo.Courses", "Id");
        }
    }
}
