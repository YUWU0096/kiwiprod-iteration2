namespace Iteration_2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class webModel : DbContext
    {
        public webModel()
            : base("name=webModel")
        {
        }

        public virtual DbSet<AgeGroup> AgeGroup { get; set; }
        public virtual DbSet<English_Profeciency> English_Profeciency { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Highest_education> Highest_education { get; set; }
        public virtual DbSet<prediction> prediction { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgeGroup>()
                .Property(e => e.ageGroup1)
                .IsUnicode(false);

            modelBuilder.Entity<English_Profeciency>()
                .Property(e => e.degree)
                .IsUnicode(false);

            modelBuilder.Entity<Gender>()
                .Property(e => e.Gender1)
                .IsUnicode(false);

            modelBuilder.Entity<Highest_education>()
                .Property(e => e.degree)
                .IsUnicode(false);

            modelBuilder.Entity<prediction>()
                .Property(e => e.pred_1)
                .IsUnicode(false);

            modelBuilder.Entity<prediction>()
                .Property(e => e.pred_2)
                .IsUnicode(false);

            modelBuilder.Entity<prediction>()
                .Property(e => e.pred_3)
                .IsUnicode(false);

            modelBuilder.Entity<prediction>()
                .Property(e => e.pred_4)
                .IsUnicode(false);

            modelBuilder.Entity<prediction>()
                .Property(e => e.pred_5)
                .IsUnicode(false);
        }
    }
}
