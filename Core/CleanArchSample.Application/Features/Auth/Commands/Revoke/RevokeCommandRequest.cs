using System.ComponentModel;
using MediatR;

namespace CleanArchSample.Application.Features.Auth.Commands.Revoke
{
    public class RevokeCommandRequest : IRequest<Unit>
    {
        [DefaultValue("asdasd@asdasd.com")]
        public string Email { get; set; }
    }
}
