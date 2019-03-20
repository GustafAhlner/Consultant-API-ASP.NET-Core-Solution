using Consultant_API_ASP.NET_Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Consultant_API_ASP.NET_Core.Data
{
    public class ConsultantContext : DbContext
    {
        public ConsultantContext(DbContextOptions<ConsultantContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsultantCompetence>()
                .HasKey(sc => new { sc.CompetenceId, sc.ConsultantId });
        }

        public DbSet<Consultant> consultants { get; set; }
        public DbSet<Competence> competences { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<ConsultantCompetence> ConsultantCompetences { get; set; }
    }
}
