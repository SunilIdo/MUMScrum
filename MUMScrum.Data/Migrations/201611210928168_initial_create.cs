namespace MUMScrum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        Phone = c.Int(nullable: false),
                        Email = c.String(),
                        Status = c.String(),
                        Sex = c.String(),
                        Salary = c.Double(nullable: false),
                        Bonus = c.Double(nullable: false),
                        Commission = c.Double(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        RoleEnum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.LoginUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedById = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.CreatedById)
                .ForeignKey("dbo.Employees", t => t.ModifiedById)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.ReleaseBacklogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ProjectId = c.Int(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedById = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.CreatedById)
                .ForeignKey("dbo.Employees", t => t.ModifiedById)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.Sprints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        ReleaseBacklogId = c.Int(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedById = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.CreatedById)
                .ForeignKey("dbo.Employees", t => t.ModifiedById)
                .ForeignKey("dbo.ReleaseBacklogs", t => t.ReleaseBacklogId)
                .Index(t => t.ReleaseBacklogId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.UserStories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserStoryType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        DeveloperId = c.Int(),
                        TesterId = c.Int(),
                        DeveloperEstimate = c.Decimal(precision: 18, scale: 2),
                        TesterEstimate = c.Decimal(precision: 18, scale: 2),
                        ReleaseBacklogId = c.Int(),
                        ProjectId = c.Int(),
                        SprintId = c.Int(),
                        CreatedById = c.Int(),
                        ModifiedById = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.CreatedById)
                .ForeignKey("dbo.Employees", t => t.DeveloperId)
                .ForeignKey("dbo.Employees", t => t.ModifiedById)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.ReleaseBacklogs", t => t.ReleaseBacklogId)
                .ForeignKey("dbo.Sprints", t => t.SprintId)
                .ForeignKey("dbo.Employees", t => t.TesterId)
                .Index(t => t.DeveloperId)
                .Index(t => t.TesterId)
                .Index(t => t.ReleaseBacklogId)
                .Index(t => t.ProjectId)
                .Index(t => t.SprintId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.WorkLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        WorkCompleted = c.Int(nullable: false),
                        UserStoryId = c.Int(nullable: false),
                        CreatedById = c.Int(),
                        ModifiedById = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.CreatedById)
                .ForeignKey("dbo.Employees", t => t.ModifiedById)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .ForeignKey("dbo.UserStories", t => t.UserStoryId)
                .Index(t => t.UserId)
                .Index(t => t.UserStoryId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkLogs", "UserStoryId", "dbo.UserStories");
            DropForeignKey("dbo.WorkLogs", "UserId", "dbo.Employees");
            DropForeignKey("dbo.WorkLogs", "ModifiedById", "dbo.Employees");
            DropForeignKey("dbo.WorkLogs", "CreatedById", "dbo.Employees");
            DropForeignKey("dbo.UserStories", "TesterId", "dbo.Employees");
            DropForeignKey("dbo.UserStories", "SprintId", "dbo.Sprints");
            DropForeignKey("dbo.UserStories", "ReleaseBacklogId", "dbo.ReleaseBacklogs");
            DropForeignKey("dbo.UserStories", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.UserStories", "ModifiedById", "dbo.Employees");
            DropForeignKey("dbo.UserStories", "DeveloperId", "dbo.Employees");
            DropForeignKey("dbo.UserStories", "CreatedById", "dbo.Employees");
            DropForeignKey("dbo.Sprints", "ReleaseBacklogId", "dbo.ReleaseBacklogs");
            DropForeignKey("dbo.Sprints", "ModifiedById", "dbo.Employees");
            DropForeignKey("dbo.Sprints", "CreatedById", "dbo.Employees");
            DropForeignKey("dbo.ReleaseBacklogs", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ReleaseBacklogs", "ModifiedById", "dbo.Employees");
            DropForeignKey("dbo.ReleaseBacklogs", "CreatedById", "dbo.Employees");
            DropForeignKey("dbo.Projects", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Projects", "ModifiedById", "dbo.Employees");
            DropForeignKey("dbo.Projects", "CreatedById", "dbo.Employees");
            DropForeignKey("dbo.LoginUsers", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.WorkLogs", new[] { "ModifiedById" });
            DropIndex("dbo.WorkLogs", new[] { "CreatedById" });
            DropIndex("dbo.WorkLogs", new[] { "UserStoryId" });
            DropIndex("dbo.WorkLogs", new[] { "UserId" });
            DropIndex("dbo.UserStories", new[] { "ModifiedById" });
            DropIndex("dbo.UserStories", new[] { "CreatedById" });
            DropIndex("dbo.UserStories", new[] { "SprintId" });
            DropIndex("dbo.UserStories", new[] { "ProjectId" });
            DropIndex("dbo.UserStories", new[] { "ReleaseBacklogId" });
            DropIndex("dbo.UserStories", new[] { "TesterId" });
            DropIndex("dbo.UserStories", new[] { "DeveloperId" });
            DropIndex("dbo.Sprints", new[] { "ModifiedById" });
            DropIndex("dbo.Sprints", new[] { "CreatedById" });
            DropIndex("dbo.Sprints", new[] { "ReleaseBacklogId" });
            DropIndex("dbo.ReleaseBacklogs", new[] { "ModifiedById" });
            DropIndex("dbo.ReleaseBacklogs", new[] { "CreatedById" });
            DropIndex("dbo.ReleaseBacklogs", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "ModifiedById" });
            DropIndex("dbo.Projects", new[] { "CreatedById" });
            DropIndex("dbo.Projects", new[] { "EmployeeId" });
            DropIndex("dbo.LoginUsers", new[] { "EmployeeId" });
            DropTable("dbo.WorkLogs");
            DropTable("dbo.UserStories");
            DropTable("dbo.Sprints");
            DropTable("dbo.ReleaseBacklogs");
            DropTable("dbo.Projects");
            DropTable("dbo.LoginUsers");
            DropTable("dbo.Employees");
        }
    }
}
