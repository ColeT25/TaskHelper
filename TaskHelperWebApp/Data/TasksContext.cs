using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskHelperWebApp;

namespace TaskHelperWebApp.Data
{
    public class TasksContext : DbContext
    {
        public TasksContext (DbContextOptions<TasksContext> options)
            : base(options)
        {
        }

        public DbSet<TaskHelperWebApp.Tasks> Tasks { get; set; } = default!;
        public DbSet<TaskHelperWebApp.Boards> Boards { get; set; } = default!;
        public DbSet<TaskHelperWebApp.Projects> Projects { get; set; } = default!;
    }
}
