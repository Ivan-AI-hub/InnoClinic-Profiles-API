using Microsoft.AspNetCore.Mvc;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate;
using ProfilesAPI.Presentation.Models.ErrorModels;

namespace ProfilesAPI.Presentation.Controllers
{
    [ApiController]
    [Route("/receptionists/")]
    public class ReceptionistController : ControllerBase
    {
        private readonly IReceptionistService _receptionistService;
        public ReceptionistController(IReceptionistService receptionistService)
        {
            _receptionistService = receptionistService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReceptionistDTO), 201)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateReceptionistModel createModel, CancellationToken cancellationToken = default)
        {
            var receptionist = await _receptionistService.CreateReceptionistAsync(createModel, cancellationToken);
            return CreatedAtAction(nameof(GetReceptionistAsync), new { email = createModel.Info.Email }, receptionist);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> EditAsync(Guid id, [FromBody] EditReceptionistModel editModel, CancellationToken cancellationToken = default)
        {
            await _receptionistService.EditReceptionistAsync(id, editModel, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _receptionistService.DeleteReceptionistAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReceptionistDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetReceptionistAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var receptionist = await _receptionistService.GetReceptionistAsync(id, cancellationToken);
            return Ok(receptionist);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(ReceptionistDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetReceptionistAsync(string email, CancellationToken cancellationToken = default)
        {
            var receptionist = await _receptionistService.GetReceptionistAsync(email, cancellationToken);
            return Ok(receptionist);
        }

        [HttpGet("{pageSize}/{pageNumber}")]
        [ProducesResponseType(typeof(ReceptionistDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetReceptionists(int pageSize, int pageNumber, [FromQuery] ReceptionistFiltrationModel filtrationModel, CancellationToken cancellationToken = default)
        {
            var receptionist = await _receptionistService.GetReceptionistsAsync(new Page(pageNumber, pageSize), filtrationModel, cancellationToken);
            return Ok(receptionist);
        }
    }
}
