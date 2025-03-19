using DevFreela.Application.InputModels.User;
using DevFreela.Application.ViewModels.User;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Services.Interfaces;

public interface IUserService
{
    UserViewModel? GetUser(int id);
    int Create(CreateUserInputModel inputModel);
}
