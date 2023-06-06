using Microsoft.AspNetCore.Mvc;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Presentation.Models.ErrorModels;

namespace ProfilesAPI.Presentation.Controllers
{
    [ApiController]
    [Route("/doctors/")]
    public class DoctorController : ControllerBase
    {
        private IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DoctorDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> CreateAsync(CreateDoctorModel createModel, CancellationToken cancellationToken = default)
        {
            var doctor = await _doctorService.CreateDoctorAsync(createModel, cancellationToken);
            return Ok(doctor);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> EditAsync(Guid id, EditDoctorModel editModel, CancellationToken cancellationToken = default)
        {
            await _doctorService.EditDoctorAsync(id, editModel, cancellationToken);
            return Accepted();
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(202)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> EditStatusAsync(Guid id, WorkStatusDTO workStatus, CancellationToken cancellationToken = default)
        {
            await _doctorService.EditDoctorStatusAsync(id, workStatus, cancellationToken);
            return Accepted();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DoctorDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetDoctorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctor = await _doctorService.GetDoctorAsync(id, cancellationToken);
            return Ok(doctor);
        }

        [HttpGet("{pageSize}/{pageNumber}")]
        [ProducesResponseType(typeof(DoctorDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult GetDoctorsAsync(int pageSize, int pageNumber, [FromQuery] DoctorFiltrationModel filtrationModel)
        {
            var doctors = _doctorService.GetDoctors(new Page(pageNumber, pageSize), filtrationModel);
            return Ok(doctors);
        }
    }
}
