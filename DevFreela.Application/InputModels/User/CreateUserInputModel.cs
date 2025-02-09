namespace DevFreela.Application.InputModels.User;

public class CreateUserInputModel
{
    public CreateUserInputModel(string fullName, string password, string email, DateTime birthDate)
    {
        FullName = fullName;
        Password = password;
        Email = email;
        BirthDate = birthDate;
    }

    public string FullName { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
}
