using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(projectDb => projectDb.Id == id);

            project.Finish();

            _dbContext.SaveChanges();
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;

            return projects
                .Select(projectVM => new ProjectViewModel(projectVM.Id, projectVM.Title, projectVM.CreatedAt))
                .ToList();
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            var project = _dbContext.Projects
                .Include(project => project.Client)
                .Include(project => project.Freelancer)
                .SingleOrDefault(projectDb => projectDb.Id == id);

            if (project == null) return null;

            var projectDetailsVM = new ProjectDetailsViewModel(
                project.Id,
                project.Description,
                project.Title,
                project.TotalCost,
                project.StartedAt,
                project.FinishedAt,
                project.Client.FullName,
                project.Freelancer.FullName);

            return projectDetailsVM;
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(projectDb => projectDb.Id == id);

            project.Start();

            _dbContext.SaveChanges();
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(projectDb => projectDb.Id == inputModel.Id);

            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);

            _dbContext.SaveChanges();
        }
    }
}
