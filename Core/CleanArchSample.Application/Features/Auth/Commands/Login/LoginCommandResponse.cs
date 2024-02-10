namespace CleanArchSample.Application.Features.Auth.Commands.Login
{
    public class LoginCommandResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiry { get; set; }
    }
}
