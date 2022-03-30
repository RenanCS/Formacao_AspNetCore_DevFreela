using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Infrastructure.Persistence;
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
