using DevFreela.Application.InputModels;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IUserService
    {
        int Create(CreateUserInputModel inputModel);
    }
}
