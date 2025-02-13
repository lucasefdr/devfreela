using DevFreela.Application.InputModels.User;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly DevFreelaDbContext _context;

    public UserService(DevFreelaDbContext context)
    {
        _context = context;
    }

    public int Create(CreateUserInputModel inputModel)
    {
        var newUser = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate);
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return newUser.Id;
    }

    public UserViewModel? GetUser(int id)
    {
        var user = _context.Users.SingleOrDefault(u => u.Id == id);

        if (user is null)
            return null;

        var userViewModel = new UserViewModel(user.FullName, user.Email);
        return userViewModel;
    }
}
