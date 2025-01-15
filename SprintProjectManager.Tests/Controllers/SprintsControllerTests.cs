using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SprintProjectManager.Controllers;
using SprintProjectManager.Data;
using SprintProjectManager.Models;

public class SprintsControllerTests : IDisposable
{
    private readonly SprintProjectManagerContext _context;
    private readonly SprintsController _controller;

    public SprintsControllerTests()
    {
        var options = new DbContextOptionsBuilder<SprintProjectManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SprintProjectManagerContext(options);
        SeedDatabase();

        _controller = new SprintsController(_context);
    }

    private void SeedDatabase()
    {
        _context.Sprint.AddRange(
            new Sprint
            {
                Id = 1,
                Name = "Sprint 1",
                Status = "Active",
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now.AddDays(10),
                Goal = "Complete initial project setup"
            },
            new Sprint
            {
                Id = 2,
                Name = "Sprint 2",
                Status = "Completed",
                StartDate = DateTime.Now.AddDays(-20),
                EndDate = DateTime.Now.AddDays(-10),
                Goal = "Develop core features"
            }
        );
        _context.SaveChanges();
    }

    // details test
    [Fact]
    public async Task Details_ReturnsViewResult_WithSprint()
    {
        var result = await _controller.Details(1);

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Sprint>(viewResult.Model);
        Assert.Equal(1, model.Id);
    }

    // create test
    [Fact]
    public async Task Create_ReturnsViewResult()
    {
        // act
        var result =  _controller.Create();

        // assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.Model); // tests to make sure no model is passed
    }

    // create POST test
    [Fact]
    public async Task Create_Post_AddsSprintAndRedirectsToIndex()
    {
        // arrange
        var newSprint = new Sprint
        {
            Id = 3,
            Name = "New Sprint",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(10),
            Goal = "Complete project tasks",
            Status = "In Progress"
        };

        // act
        var result = await _controller.Create(newSprint);

        // assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);

        // verify
        var addedSprint = _context.Sprint.FirstOrDefault(s => s.Id == newSprint.Id);
        Assert.NotNull(addedSprint);
        Assert.Equal("New Sprint", addedSprint.Name);
        Assert.Equal("Complete project tasks", addedSprint.Goal);
    }

    // delete test
    [Fact]
    public async Task Delete_ReturnsViewResult_WithSprint()
    {
        // act
        var result = await _controller.Delete(1);

        // assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Sprint>(viewResult.Model);
        Assert.Equal(1, model.Id);
    }

    // delete POST test
    [Fact]
    public async Task DeleteConfirmed_DeletesSprintAndRedirectsToIndex()
    {
        // arrange
        var initialSprintCount = _context.Sprint.Count();

        // act
        var result = await _controller.DeleteConfirmed(1);

        // assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Equal(initialSprintCount - 1, _context.Sprint.Count()); 
        Assert.Null(_context.Sprint.FirstOrDefault(s => s.Id == 1)); // tests to make sure id 1 no longer exist
    }


    // disposes context after test
    public void Dispose()
    {
        _context.Dispose();
    }
}
