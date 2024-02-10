using System.ComponentModel;
using MediatR;

namespace CleanArchSample.Application.Features.Auth.Commands.Login
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        [DefaultValue("asdasd@asdasd.com")]
        public string Email { get; set; }
        [DefaultValue("123456")]
        public string Password { get; set; }
    }
}
