namespace Linq_to_Collection
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class crcms : DbContext
    {
        public crcms()
            : base("name=crcms")
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<Timer> Timer { get; set; }
        public virtual DbSet<TimerArchive> TimerArchive { get; set; }
        public virtual DbSet<TimerInactivity> TimerInactivity { get; set; }
        public virtual DbSet<TimerInactivityArchive> TimerInactivityArchive { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.HoursMachines)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Document>()
                .Property(e => e.PartsCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Document>()
                .Property(e => e.WorkCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Document>()
                .Property(e => e.ConsumablesCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Document>()
                .Property(e => e.ComponentHours)
                .HasPrecision(19, 4);
        }
    }
}
