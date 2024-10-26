using Microsoft.EntityFrameworkCore;
using TODOList.Domain.Entities;

namespace TODOList.Infrastructure
{
    public class TodoContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().ToTable("Todos");
        }

    }
}
