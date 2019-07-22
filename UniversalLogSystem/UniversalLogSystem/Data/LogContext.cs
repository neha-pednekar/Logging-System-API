using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UniversalLogSystem.Models
{
    public class LogContext : DbContext
    {
        public LogContext (DbContextOptions<LogContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogEntity>()
                .Property(b => b.LoggingDate)
                .HasDefaultValueSql("getdate()");
        }

        public DbSet<UniversalLogSystem.Models.LogEntity> LogEntity { get; set; }
    }
}
