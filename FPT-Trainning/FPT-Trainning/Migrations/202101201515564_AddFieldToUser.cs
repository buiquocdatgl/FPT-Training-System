namespace FPT_Trainning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainees", "ProgramLanguage", c => c.String());
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.Trainers", "Education", c => c.String());
            DropColumn("dbo.Trainees", "Department");
            DropColumn("dbo.Trainers", "Name");
            DropColumn("dbo.Trainers", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainers", "Email", c => c.String());
            AddColumn("dbo.Trainers", "Name", c => c.String());
            AddColumn("dbo.Trainees", "Department", c => c.String());
            DropColumn("dbo.Trainers", "Education");
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.Trainees", "ProgramLanguage");
        }
    }
}
