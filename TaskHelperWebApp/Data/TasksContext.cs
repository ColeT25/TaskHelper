using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskHelperWebApp.Models;

namespace TaskHelperWebApp.Data
{
    public class TasksContext : DbContext
    {
        public TasksContext (DbContextOptions<TasksContext> options)
            : base(options)
        {
        }

        public DbSet<Tasks> Tasks { get; set; } = default!;
        public DbSet<Boards> Boards { get; set; } = default!;
        public DbSet<Projects> Projects { get; set; } = default!;
    }
}
