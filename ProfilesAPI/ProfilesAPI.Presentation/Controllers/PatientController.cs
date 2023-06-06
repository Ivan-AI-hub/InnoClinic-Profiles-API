using Microsoft.AspNetCore.Mvc;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels.PatientAggregate;
using ProfilesAPI.Presentation.Models.ErrorModels;

namespace ProfilesAPI.Presentation.Controllers
{
    [ApiController]
    [Route("/patients/")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PatientDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> CreateAsync([FromForm] CreatePatientModel createModel, CancellationToken cancellationToken = default)
        {
            var patient = await _patientService.CreatePatientAsync(createModel, cancellationToken);
            return Ok(patient);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> EditAsync(Guid id, [FromForm] EditPatientModel editModel, CancellationToken cancellationToken = default)
        {
            await _patientService.EditPatientAsync(id, editModel, cancellationToken);
            return Accepted();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _patientService.DeletePatientAsync(id, cancellationToken);
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PatientDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetPatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _patientService.GetPatientAsync(id, cancellationToken);
            return Ok(patient);
        }

        [HttpGet("{pageSize}/{pageNumber}")]
        [ProducesResponseType(typeof(PatientDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult GetPatients(int pageSize, int pageNumber, [FromQuery] PatientFiltrationModel filtrationModel)
        {
            var patient = _patientService.GetPatients(new Page(pageNumber, pageSize), filtrationModel);
            return Ok(patient);
        }
    }
}
