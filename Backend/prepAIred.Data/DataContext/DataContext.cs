using Microsoft.EntityFrameworkCore;

namespace prepAIred.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<InterviewSession> InterviewSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HRInterview>().ToTable("HRInterviews");
            modelBuilder.Entity<TechnicalInterview>().ToTable("TechnicalInterviews");

            modelBuilder.Entity<Interview>()
                .HasOne(i => i.User)
                .WithMany(u => u.Interviews)
                .HasForeignKey(i => i.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Interview>()
                .HasOne(i => i.InterviewSession)
                .WithMany(s => s.Interviews)
                .HasForeignKey(i => i.InterviewSessionID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(n => n.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(n => n.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InterviewSession>()
                .HasOne(s => s.User)
                .WithMany(u => u.InterviewSessions)
                .HasForeignKey(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
