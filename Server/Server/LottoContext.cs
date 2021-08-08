using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Server
{
    public partial class LottoContext : DbContext
    {
        public LottoContext()
        {
        }

        public LottoContext(DbContextOptions<LottoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Circulation> Circulations { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-IK01QNC;Database=Lotto;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Circulation>(entity =>
            {
                entity.HasKey(e => e.Circulation1)
                    .HasName("PK__Circulat__415A71F538052BEA");

                entity.Property(e => e.Circulation1)
                    .ValueGeneratedNever()
                    .HasColumnName("Circulation");

                entity.Property(e => e.WinnerPosition)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Variant)
                    .HasName("PK__Tickets__45CF3AF86CB7162B");

                entity.Property(e => e.SelectedNum)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.СirculationNumNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.СirculationNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tickets__Сircula__6B24EA82");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
