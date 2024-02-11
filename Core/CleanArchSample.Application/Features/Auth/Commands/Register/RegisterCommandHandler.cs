using AutoMapper;
using CleanArchSample.Application.Features.Auth.Rules;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArchSample.Application.Features.Auth.Commands.Register
{
    internal class RegisterCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        AuthRule authRole,
        UserManager<User> userManger,
        RoleManager<Role> roleManager)
        : CqrsHandlerBase(unitOfWork, mapper, httpContextAccessor), IRequestHandler<RegisterCommandRequest, Unit>
    {
        public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            await authRole.UserShouldNotBeExist(request.Email!);
            var user = Mapper.Map<User>(request);
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
