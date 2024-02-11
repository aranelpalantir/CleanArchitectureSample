namespace CleanArchSample.Application.Features.Auth.Commands.Login
{
    internal class LoginCommandResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiry { get; set; }
    }
}
