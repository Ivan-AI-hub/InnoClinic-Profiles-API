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
        [ProducesResponseType(typeof(ReceptionistDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> CreateAsync(CreateReceptionistModel createModel, CancellationToken cancellationToken = default)
        {
            var receptionist = await _receptionistService.CreateReceptionistAsync(createModel, cancellationToken);
            return Ok(receptionist);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> EditAsync(Guid id, EditReceptionistModel editModel, CancellationToken cancellationToken = default)
        {
            await _receptionistService.EditReceptionistAsync(id, editModel, cancellationToken);
            return Accepted();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _receptionistService.DeleteReceptionistAsync(id, cancellationToken);
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReceptionistDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetReceptionistAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var receptionist = await _receptionistService.GetReceptionistAsync(id, cancellationToken);
            return Ok(receptionist);
        }

        [HttpGet("{pageSize}/{pageNumber}")]
        [ProducesResponseType(typeof(ReceptionistDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult GetReceptionists(int pageSize, int pageNumber, [FromQuery] ReceptionistFiltrationModel filtrationModel)
        {
            var receptionist = _receptionistService.GetReceptionists(new Page(pageNumber, pageSize), filtrationModel);
            return Ok(receptionist);
        }
    }
}
