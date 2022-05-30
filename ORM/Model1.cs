using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ORM
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Auteur> Auteur { get; set; }
        public virtual DbSet<Livre> Livre { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auteur>()
                .HasMany(e => e.Livre)
                .WithRequired(e => e.Auteur)
                .WillCascadeOnDelete(false);
        }
    }
}
