using ExamenPOOU1.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamenPOOU1.Database;

        public class ExamenContext : DbContext
        {
            public ExamenContext(DbContextOptions options) : base(options)
            {
            }

            public DbSet<CategoryEntity> Categories { get; set; }
        }
}
