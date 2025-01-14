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
                        Status = "Completed"
                    },
                    new Sprint
                    {
                        Name = "Sprint 1",
                        StartDate = DateTime.Parse("2025-1-27"),
                        EndDate = DateTime.Parse("2025-2-10"),
                        Goal = "Complete database schema and initial API integration",
                        Status = "Completed"
                    },
                    new Sprint
                    {
                        Name = "Sprint 1",
                        StartDate = DateTime.Parse("2025-2-11"),
                        EndDate = DateTime.Parse("2025-2-24"),
                        Goal = "Implement user authentication and authorization",
                        Status = "Incomplete"
                    },
                    new Sprint
                    {
                        Name = "Sprint 2",
                        StartDate = DateTime.Parse("2025-2-25"),
                        EndDate = DateTime.Parse("2025-3-10"),
                        Goal = "Finalize backend services and deploy to staging",
                        Status = "In Progress"
                    },
                    new Sprint
                    {
                        Name = "Sprint 2",
                        StartDate = DateTime.Parse("2025-3-11"),
                        EndDate = DateTime.Parse("2025-3-24"),
                        Goal = "Conduct QA testing and bug fixes",
                        Status = "In Progress"
                    },
                    new Sprint
                    {
                        Name = "Sprint 2",
                        StartDate = DateTime.Parse("2025-3-25"),
                        EndDate = DateTime.Parse("2025-4-8"),
                        Goal = "Prepare for production deployment",
                        Status = "Completed"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
