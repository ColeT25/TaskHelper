using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskHelperWebApp;

namespace TaskHelperWebApp.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext (DbContextOptions<TaskContext> options)
            : base(options)
        {
        }

        public DbSet<TaskHelperWebApp.Tasks> Tasks { get; set; } = default!;
    }
}
