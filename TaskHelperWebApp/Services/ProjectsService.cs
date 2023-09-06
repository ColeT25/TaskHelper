using TaskHelperWebApp.Data;
using TaskHelperWebApp.Models;
using TaskHelperWebApp.Services.Interfaces;

namespace TaskHelperWebApp.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly TasksContext _context;
        public ProjectsService(TasksContext context) {
            _context = context;
        }

        public Projects? CreateProject(string name, string description)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProject(Guid projectID)
        {
            throw new NotImplementedException();
        }

        public Projects? GetProjectByID(Guid ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a list of projects
        /// </summary>
        /// <param name="onlyActiveProjects">Get only active projects or all projects</param>
        /// <returns>A list of projects returned from the query</returns>
        public List<Projects> GetProjects(bool onlyActiveProjects = true)
        {
                if (onlyActiveProjects)
                    return _context.Projects.Where(p => p.ClosedDate == null).ToList();
                else
                    return _context.Projects.ToList();
        }

        public Projects? UpdateProject(Guid projectIDToUpdate, Projects newProject)
        {
            throw new NotImplementedException();
        }

    }
}
