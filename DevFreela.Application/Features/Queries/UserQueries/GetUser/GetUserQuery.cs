using DevFreela.Application.DTOs.ViewModels.User;
using MediatR;

namespace DevFreela.Application.Features.Queries.UserQueries.GetUser;

public class GetUserQuery(int id) : IRequest<UserViewModel?>
{
    public int Id { get; set; } = id;
}
