using Microsoft.EntityFrameworkCore;
using PQT.CC.Models;

namespace PQT.CC.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Results> Results{ get; set; }
    }
}
