using Quiz.DAL.Entities;
using System;
using System.Data.Entity;
using Quiz.DAL.Entities.UserResults;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Quiz.DAL.Context
{
  public class EFContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<TestResult> TestResults { get; set; }
    public DbSet<ResultDetails> ResultDetails { get; set; }

    static EFContext()
    {
      Database.SetInitializer(new QuizDbInitializer());
    }

    public EFContext(string connectionString)
        : base(connectionString)
    {
    }

    public EFContext()
        : base("DefaultConnection")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Question>()
        .HasMany(q => q.Answers)
        .WithRequired(a => a.Question)
        .HasForeignKey(a => a.QuestionId)
        .WillCascadeOnDelete();

      modelBuilder.Entity<Test>()
        .HasMany(t => t.Questions)
        .WithRequired(q => q.Test)
        .HasForeignKey(q => q.TestId)
        .WillCascadeOnDelete();

      #region UserResult

      modelBuilder.Entity<Test>()
        .HasMany(t => t.TestResults)
        .WithRequired(r => r.Test)
        .HasForeignKey(r => r.TestId)
        .WillCascadeOnDelete();

      modelBuilder.Entity<ApplicationUser>()
        .HasMany(u => u.TestResults)
        .WithRequired(r => r.User)
        .HasForeignKey(r => r.UserId)
        .WillCascadeOnDelete();

      modelBuilder.Entity<TestResult>()
        .HasMany(r => r.Details)
        .WithRequired(d => d.Result)
        .HasForeignKey(d => d.ResultId)
        .WillCascadeOnDelete();

      modelBuilder.Entity<Question>()
        .HasMany(q => q.ResultDetails)
        .WithRequired(d => d.Question)
        .HasForeignKey(d => d.QuestionId)
        .WillCascadeOnDelete(false);

      modelBuilder.Entity<Answer>()
        .HasMany(a => a.ResultDetails)
        .WithRequired(d => d.Answer)
        .HasForeignKey(d => d.AnswerId)
        .WillCascadeOnDelete(false);

      #endregion
    }
  }

  public class QuizDbInitializer : DropCreateDatabaseIfModelChanges<EFContext>
  {
    protected override void Seed(EFContext db)
    {
      if (db == null)
        throw new ArgumentNullException("db");

      var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
      var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

      var adminRole = new IdentityRole { Name = "admin" };
      var userRole = new IdentityRole { Name = "user" };

      roleManager.Create(adminRole);
      roleManager.Create(userRole);

      var admin = new ApplicationUser
      {
        Email = "admin@gmail.com",
        UserName = "admin",
        Name = "Ivan Ivanch",
      };
      string password = "Admin/3000";

      var result = userManager.Create(admin, password);
      if (result.Succeeded)
      {
        userManager.AddToRole(admin.Id, adminRole.Name);
        userManager.AddToRole(admin.Id, userRole.Name);
      }

      base.Seed(db);

      //db.Tests.Add(new Test { Name = "Vegetables" });
      //db.Questions.Add(new Question
      //{
      //    Name = "Сucumber",
      //    Text = "What color is cucumber?",
      //    Type = QuestionType.Single,

      //    TestId = 1
      //});
      //db.Answers.Add(new Answer
      //{
      //    Text = "Green",
      //    QuestionId = 1
      //});
      //db.Answers.Add(new Answer
      //{
      //    Text = "Red",
      //    QuestionId = 1
      //});
      //db.Answers.Add(new Answer
      //{
      //    Text = "Blue",
      //    QuestionId = 1
      //});
      //db.Questions.



      //db.Tests.Add(new Test { Name = "Fruits" });

      db.SaveChanges();
    }
  }
}
