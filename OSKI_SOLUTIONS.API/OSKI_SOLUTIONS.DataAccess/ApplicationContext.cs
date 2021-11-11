using Microsoft.EntityFrameworkCore;
using OSKI_SOLUTIONS.DataAccess.Entities;
using OSKI_SOLUTIONS.DataAccess.Entities.Authorization;
using OSKI_SOLUTIONS.DataAccess.Entities.Tests;
using OSKI_SOLUTIONS.DataAccess.Entities.Tests.Session;
using OSKI_SOLUTIONS.Helpers.Managers;

namespace OSKI_SOLUTIONS.DataAccess
{
    public class AuthContext<AUser> : DbContext where AUser: AuthUser
    {
        public DbSet<ActiveRefreshToken<AUser>> ActiveRefreshTokens { get; set; }
        public DbSet<AUser> Users { get; set; }

        public AuthContext(DbContextOptions options) : base(options){}
    }

    public class ApplicationContext: AuthContext<User>
    {
        // test
        public DbSet<OptionOfQuestion> OptionsOfQuestions { get; set; }
        public DbSet<QuestionOfTest> QuestionsOfTests { get; set; }
        public DbSet<Test> Tests { get; set; }

        // sessions
        public DbSet<OptionOfQuestionInSession> OptionsOfQuestionsInSessions { get; set; }
        public DbSet<QuestionOfTest> QuestionOfSession { get; set; }
        public DbSet<SessionOfTest> SessionsOfTests { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User { 
                Id = "admin",
                Login = "admin",
                PasswordHash = PasswordHandler.CreatePasswordHash("admin"),
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
