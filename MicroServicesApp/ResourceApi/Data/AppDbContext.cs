using Microsoft.EntityFrameworkCore;
using ResourceApi.Models;

namespace ResourceApi.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Quest> Quests { get; set; } 
        public DbSet<TestModel> testModels { get; set; }
    }
}
