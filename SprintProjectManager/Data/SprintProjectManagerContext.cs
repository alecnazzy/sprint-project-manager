using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SprintProjectManager.Models;

namespace SprintProjectManager.Data
{
    public class SprintProjectManagerContext : DbContext
    {
        public SprintProjectManagerContext (DbContextOptions<SprintProjectManagerContext> options)
            : base(options)
        {
        }

        public DbSet<SprintProjectManager.Models.Sprint> Sprint { get; set; } = default!;
    }
}
