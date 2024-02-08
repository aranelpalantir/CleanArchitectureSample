using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchSample.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
