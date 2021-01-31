namespace FPT_Trainning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNullFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Trainees", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Trainers", "CourseId", "dbo.Courses");
            DropIndex("dbo.Courses", new[] { "CategoryId" });
            DropIndex("dbo.Trainees", new[] { "CourseId" });
            DropIndex("dbo.Trainers", new[] { "CourseId" });
            AlterColumn("dbo.Courses", "CategoryId", c => c.Int());
            AlterColumn("dbo.Trainees", "CourseId", c => c.Int());
            AlterColumn("dbo.Trainers", "CourseId", c => c.Int());
            CreateIndex("dbo.Courses", "CategoryId");
            CreateIndex("dbo.Trainees", "CourseId");
            CreateIndex("dbo.Trainers", "CourseId");
            AddForeignKey("dbo.Courses", "CategoryId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Trainees", "CourseId", "dbo.Courses", "Id");
            AddForeignKey("dbo.Trainers", "CourseId", "dbo.Courses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainers", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Trainees", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Trainers", new[] { "CourseId" });
            DropIndex("dbo.Trainees", new[] { "CourseId" });
            DropIndex("dbo.Courses", new[] { "CategoryId" });
            AlterColumn("dbo.Trainers", "CourseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Trainees", "CourseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trainers", "CourseId");
            CreateIndex("dbo.Trainees", "CourseId");
            CreateIndex("dbo.Courses", "CategoryId");
            AddForeignKey("dbo.Trainers", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Trainees", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Courses", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
