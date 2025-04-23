using KanbanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Board> Boards { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}
