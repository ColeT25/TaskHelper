using TaskHelperWebApp.Services;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using TaskHelperWebApp.Services.Interfaces;
using TaskHelperWebApp.Models;
using Microsoft.EntityFrameworkCore;
using TaskHelperWebApp.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Policy;

namespace TaskHelperTests.ServiceTests
{
    /// <summary>
    /// Contains Unit tests for the ProjectsService class
    /// which implements the IProjectsService interface
    /// </summary>
    [TestClass]
    public class ProjectsServiceTests
    {
        private readonly DbContextOptions<TasksContext> _contextOptions = default!;
        private readonly ProjectsService _projectsService = default!;
        private readonly Projects _testProject1 = default!;
        private readonly Projects _testProject2 = default!;

        #region Constructor
        public ProjectsServiceTests()
        {
            // Set up fake in memory database
            _contextOptions = new DbContextOptionsBuilder<TasksContext>()
                .UseInMemoryDatabase("ProjectServicesTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            // I know this causes the context to not be disposed of correctly but it is a temp
            // fix to me not understanding how to set up this in memory db right
            var context = new TasksContext(_contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // seed with test projects, keping track of projects so I can check return values later
            _testProject1 = new Projects
            {
                ID = Guid.Parse("6af2996e-4e4c-4c89-ba80-485971c3816e"),
                Name = "Test",
                Description = "Something crazy",
                CreatedDate = DateTime.MinValue,
                ClosedDate = null
            };

            _testProject2 = new Projects
            {
                ID = Guid.Parse("dd3a027c-d820-494f-995a-ccb7ea2632ff"),
                Name = "Test 2",
                Description = "What is going on",
                CreatedDate = DateTime.MinValue,
                ClosedDate = DateTime.MaxValue
            };
            context.AddRange(_testProject1, _testProject2);
            context.SaveChanges();

            // fake DI (kinda) the service directly in here with the in memory based context
            _projectsService = new ProjectsService(context);
        }
        #endregion


        [TestMethod]
        public void TestGetProjectsOnlyActive()
        {
            var activeProjectsGotten = _projectsService.GetProjects();

            Assert.IsTrue(
                activeProjectsGotten.Count == 1
                && activeProjectsGotten.Contains(_testProject1)
                );
        }

        public void TestGetAllProjects()
        {
            var allProjectsGotten = _projectsService.GetProjects(false);

            Assert.IsTrue(
                allProjectsGotten.Count == 2 
                && allProjectsGotten.Contains(_testProject1) 
                && allProjectsGotten.Contains(_testProject2)
                );
        }
           

    }
}