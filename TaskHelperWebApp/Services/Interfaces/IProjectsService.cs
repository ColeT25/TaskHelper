namespace TaskHelperWebApp.Services.Interfaces
{
    public interface IProjectsService
    {
        public List<Projects> GetProjects(bool onlyActiveProjects);
        public Projects? GetProjectByID(Guid ID);
        public Projects? UpdateProject(Guid projectIDToUpdate, Projects newProject); //todo may not need to return projects
        public Projects? CreateProject(string name, string description);
        public bool DeleteProject(Guid projectID);
    }
}
