using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CleanArchSample.Api.Controllers
{
    public class MetaController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet("/info")]
        public ActionResult<string> Info()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var lastUpdate = System.IO.File.GetLastWriteTime(assembly.Location);
            return Ok($"Last Updated: {lastUpdate}");
        }
    }
}
