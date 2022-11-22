using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TodoContext : DbContext
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public TodoContext (DbContextOptions<TodoContext> options)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
            : base(options)
        {
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public DbSet<TodoItem> TodoItems { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
