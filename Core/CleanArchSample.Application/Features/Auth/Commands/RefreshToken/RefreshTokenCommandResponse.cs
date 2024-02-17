namespace CleanArchSample.Application.Features.Auth.Commands.RefreshToken
{
    public sealed class RefreshTokenCommandResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
