using Microsoft.EntityFrameworkCore;
using PremiumDomain.Model;

namespace PremiumDomain.Infrastructure
{
    public partial class PremiumDbContext : DbContext
    {
        public PremiumDbContext()
        {
        }

        public PremiumDbContext(DbContextOptions<PremiumDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Occupation> Occupations { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TalDBMain");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Occupation>(entity =>
            {
                entity.ToTable("Occupation");

                entity.Property(e => e.OccupationName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Rating)
                    .WithMany(p => p.Occupations)
                    .HasForeignKey(d => d.RatingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Occupation_Rating");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");

                entity.Property(e => e.Factor).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.RatingName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
