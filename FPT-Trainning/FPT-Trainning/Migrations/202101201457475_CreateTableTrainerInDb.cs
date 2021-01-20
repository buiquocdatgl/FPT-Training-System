namespace FPT_Trainning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableTrainerInDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        TraineeId = c.String(nullable: false, maxLength: 128),
                        Age = c.Int(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Experience = c.Int(nullable: false),
                        Education = c.String(),
                        Location = c.String(),
                        ToeicScore = c.Int(nullable: false),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.TraineeId)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeId)
                .Index(t => t.TraineeId);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        TrainerId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        WorkingPlace = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.TrainerId)
                .ForeignKey("dbo.AspNetUsers", t => t.TrainerId)
                .Index(t => t.TrainerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "TraineeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Trainers", "TrainerId", "dbo.AspNetUsers");
            DropIndex("dbo.Trainers", new[] { "TrainerId" });
            DropIndex("dbo.Trainees", new[] { "TraineeId" });
            DropTable("dbo.Trainers");
            DropTable("dbo.Trainees");
        }
    }
}
