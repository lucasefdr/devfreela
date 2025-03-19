using DevFreela.Application.ViewModels.User;
using MediatR;

namespace DevFreela.Application.Queries.UserQueries.GetUser;

public class GetUserQuery(int id) : IRequest<UserViewModel?>
{
    public int Id { get; set; } = id;
}
