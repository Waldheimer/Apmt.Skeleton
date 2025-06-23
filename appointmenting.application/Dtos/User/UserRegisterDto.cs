namespace appointmenting.Dtos.User
{
    public record struct UserRegisterDto(string username, string email, string password, string passwordConfirmation);
}

