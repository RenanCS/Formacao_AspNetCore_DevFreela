using DevFreela.Application.InputModels;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        void Update(UpdateProjectInputModel inputModel);
        void Start(int id);
        void Finish(int id);

    }
}
