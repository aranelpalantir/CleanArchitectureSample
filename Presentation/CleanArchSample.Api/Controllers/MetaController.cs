using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CleanArchSample.Api.Controllers
{
    public class MetaController : BaseApiController
    {
        public MetaController(IMediator mediator) : base(mediator)
        {
            
        }
        [HttpGet("/info")]
        public ActionResult<string> Info()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var lastUpdate = System.IO.File.GetLastWriteTime(assembly.Location);
            return Ok($"Last Updated: {lastUpdate}");
        }
    }
}
