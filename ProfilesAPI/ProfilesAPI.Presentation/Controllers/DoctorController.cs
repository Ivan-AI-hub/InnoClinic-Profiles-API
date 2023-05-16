using Microsoft.AspNetCore.Mvc;
using ProfilesAPI.Services.Abstraction;
using ProfilesAPI.Services.Abstraction.AggregatesModels;
using ProfilesAPI.Services.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Services.Abstraction.QueryableManipulation;

namespace ProfilesAPI.Presentation.Controllers
{
    [Route("doctors/")]
    public class DoctorController : ControllerBase
    {
        private IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateDoctorModel createModel, CancellationToken cancellationToken = default)
        {
            var doctor = await _doctorService.CreateDoctorAsync(createModel, cancellationToken);
            return Ok(doctor);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> EditAsync(Guid id, EditDoctorModel editModel, CancellationToken cancellationToken = default)
        {
            var doctor = await _doctorService.EditDoctorAsync(id, editModel, cancellationToken);
            return Ok(doctor);
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> EditStatusAsync(Guid id, WorkStatusDTO workStatus, CancellationToken cancellationToken = default)
        {
            await _doctorService.EditDoctorStatusAsync(id, workStatus, cancellationToken);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctor = await _doctorService.GetDoctorAsync(id, cancellationToken);
            return Ok(doctor);
        }

        [HttpGet]
        public IActionResult GetDoctorsAsync(Page page, DoctorFiltrationModel filtrationModel)
        {
            var doctors = _doctorService.GetDoctors(page, filtrationModel);
            return Ok(doctors);
        }
    }
}
