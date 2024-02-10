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
    public class RegisterCommandHandler : CqrsHandlerBase, IRequestHandler<RegisterCommandRequest, Unit>
    {
        private readonly AuthRule _authRole;
        private readonly UserManager<User> _userManger;
        private readonly RoleManager<Role> _roleManager;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, AuthRule authRole, UserManager<User> userManger, RoleManager<Role> roleManager) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _authRole = authRole;
            _userManger = userManger;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            await _authRole.UserShouldNotBeExist(request.Email);
            var user = Mapper.Map<User>(request);
            user.UserName = request.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            IdentityResult result = await _userManger.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("user"))
                {
                    await _roleManager.CreateAsync(new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = "user",
                        NormalizedName = "USER",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });
                }
                await _userManger.AddToRoleAsync(user, "user");
            }
            return Unit.Value;
        }
    }
}
