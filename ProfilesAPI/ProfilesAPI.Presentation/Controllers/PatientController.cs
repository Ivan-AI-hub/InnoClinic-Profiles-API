using Microsoft.AspNetCore.Mvc;
using ProfilesAPI.Services.Abstraction;
using ProfilesAPI.Services.Abstraction.AggregatesModels;
using ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate;
using ProfilesAPI.Services.Abstraction.QueryableManipulation;

namespace ProfilesAPI.Presentation.Controllers
{
    [Route("patients/")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatePatientModel createModel, CancellationToken cancellationToken = default)
        {
            var patient = await _patientService.CreatePatientAsync(createModel, cancellationToken);
            return Ok(patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(Guid id, EditPatientModel editModel, CancellationToken cancellationToken = default)
        {
            var patient = await _patientService.EditPatientAsync(id, editModel, cancellationToken);
            return Ok(patient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _patientService.DeletePatientAsync(id, cancellationToken);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _patientService.GetPatientAsync(id, cancellationToken);
            return Ok(patient);
        }

        [HttpGet]
        public IActionResult GetPatients(Page page, PatientFiltrationModel filtrationModel)
        {
            var patient = _patientService.GetPatients(page, filtrationModel);
            return Ok(patient);
        }

        [HttpGet("info")]
        public IActionResult GetPatientsInfo(Page page, PatientFiltrationModel filtrationModel)
        {
            var patient = _patientService.GetPatientsInfo(page, filtrationModel);
            return Ok(patient);
        }
    }
}
