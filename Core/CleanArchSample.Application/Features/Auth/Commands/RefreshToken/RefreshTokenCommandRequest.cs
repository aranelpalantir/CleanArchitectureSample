using MediatR;

namespace CleanArchSample.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandRequest : IRequest<RefreshTokenCommandResponse>
    {
        public string? RefreshToken { get; set; }
    }
}
