using CleanArchSample.Application.Features.Auth.Commands.Login;
using CleanArchSample.Application.Features.Auth.Commands.RefreshToken;
using CleanArchSample.Application.Features.Auth.Commands.Register;
using CleanArchSample.Application.Features.Auth.Commands.Revoke;
using CleanArchSample.Application.Features.Auth.Commands.RevokeAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchSample.Api.Controllers.v1
{
    public class AuthController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommandRequest request)
        {
            await Mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginCommandRequest request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Revoke(RevokeCommandRequest request)
        {
            await Mediator.Send(request);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> RevokeAll(RevokeAllCommandRequest request)
        {
            await Mediator.Send(request);
            return Ok();
        }
    }
}