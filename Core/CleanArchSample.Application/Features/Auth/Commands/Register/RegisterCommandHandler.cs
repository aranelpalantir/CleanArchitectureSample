using AutoMapper;
using CleanArchSample.Application.Features.Auth.Rules;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchSample.Application.Features.Auth.Commands.Register
{
    internal sealed class RegisterCommandHandler(
        IMapper mapper,
        AuthRule authRole,
        UserManager<User> userManger,
        RoleManager<Role> roleManager)
        : IRequestHandler<RegisterCommandRequest, Unit>
    {
        public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            await authRole.UserShouldNotBeExist(request.Email!);
            var user = mapper.Map<User>(request);
            user.UserName = request.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            IdentityResult result = await userManger.CreateAsync(user, request.Password!);
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("user"))
                {
                    await roleManager.CreateAsync(new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = "user",
                        NormalizedName = "USER",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });
                }
                await userManger.AddToRoleAsync(user, "user");
            }
            return Unit.Value;
        }
    }
}
