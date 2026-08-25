using Label_Exo.Models;
using Microsoft.EntityFrameworkCore;

namespace Label_Exo.Data
{
    public class LabelExoDbContext : DbContext
    {
        public LabelExoDbContext(DbContextOptions<LabelExoDbContext> options)
            : base(options)
        {
        }

        public DbSet<MusicLabel> MusicLabels { get; set; }

        public DbSet<Artiste> Artistes { get; set; }

        public DbSet<Membre> Membres { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Piste> Pistes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Artiste>()
                .HasOne(a => a.Label)
                .WithMany(l => l.Artistes)
                .HasForeignKey(a => a.LabelId);

            modelBuilder.Entity<Membre>()
                .HasOne(m => m.Artiste)
                .WithMany(a => a.Membres)
                .HasForeignKey(m => m.ArtisteId);

            modelBuilder.Entity<Album>()
                .HasOne(a => a.Artiste)
                .WithMany(a => a.Albums)
                .HasForeignKey(a => a.ArtisteId);

            modelBuilder.Entity<Piste>()
                .HasOne(p => p.Album)
                .WithMany(a => a.Pistes)
                .HasForeignKey(p => p.AlbumId);
        }
    }
}