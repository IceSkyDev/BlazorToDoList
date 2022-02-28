using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.DAL
{
    public class BlazorDbContext : DbContext
    {
        public BlazorDbContext(DbContextOptions<BlazorDbContext> options) : base(options)
        {

        }

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ToDoItem> ToDoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}