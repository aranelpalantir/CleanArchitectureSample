using MediatR;
using System.ComponentModel;

namespace CleanArchSample.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandRequest : IRequest<Unit>
    {
        [DefaultValue("asdasd asdasd")]
        public string FullName { get; set; }
        [DefaultValue("asdasd@asdasd.com")]
        public string Email { get; set; }
        [DefaultValue("123456")]
        public string Password { get; set; }
        [DefaultValue("123456")]
        public string ConfirmPassword { get; set; }
    }
}
