using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Connections
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Syllabus> Syllabuses { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Level> Levels { get; set; }
    }
}
