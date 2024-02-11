namespace CleanArchSample.Application.Features.Auth.Commands.RefreshToken
{
    internal sealed class RefreshTokenCommandResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
