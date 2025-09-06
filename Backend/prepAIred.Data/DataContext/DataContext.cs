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
            modelBuilder.Entity<RefreshToken>()
                .HasOne(n => n.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(n => n.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Interview>()
                .HasOne(i => i.User)
                .WithMany(s => s.Interviews)
                .HasForeignKey(q => q.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InterviewSession>()
                .HasOne(s => s.User)
                .WithMany(u => u.InterviewSessions)
                .HasForeignKey(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InterviewSession>()
                .HasMany(s => s.Interviews)
                .WithOne(i => i.InterviewSession)
                .HasForeignKey(i => i.InterviewSessionID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
