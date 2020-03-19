using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediatorSampleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SampleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] SampleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return FromResult(result);
        }

        private IActionResult FromResult(CommandResult result)
        {
            if (result.IsSuccess)
            {
                if (result.Value != null)
                {
                    return new JsonResult(result.Value);
                }
                return NoContent();
            }

            if (result.IsBadRequest)
            {
                return BadRequest();
            }

            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}