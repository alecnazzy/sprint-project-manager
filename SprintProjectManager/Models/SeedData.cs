using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SprintProjectManager.Data;
using System;
using System.Linq;

namespace SprintProjectManager.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SprintProjectManagerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SprintProjectManagerContext>>()))
            {
                if (context.Sprint.Any())
                {
                    return;
                }
                context.Sprint.AddRange(
                    new Sprint
                    {
                        Name = "Sprint 1",
                        StartDate = DateTime.Parse("2025-1-12"),
                        EndDate = DateTime.Parse("2025-1-26"),
                        Goal = "To start designing our database",
                        Status = "Incomplete"
                    });
                context.SaveChanges();
            }
        }
    }
}
