﻿using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(typeof(PatientDTO), 201)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePatientModel createModel, CancellationToken cancellationToken = default)
        {
            var patient = await _patientService.CreatePatientAsync(createModel, cancellationToken);
            return CreatedAtAction(nameof(GetPatientAsync), new { email = createModel.Info.Email }, patient); ;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> EditAsync(Guid id, [FromBody] EditPatientModel editModel, CancellationToken cancellationToken = default)
        {
            await _patientService.EditPatientAsync(id, editModel, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _patientService.DeletePatientAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(PatientDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetPatientAsync(string email, CancellationToken cancellationToken = default)
        {
            var patient = await _patientService.GetPatientAsync(email, cancellationToken);
            return Ok(patient);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PatientDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetPatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _patientService.GetPatientAsync(id, cancellationToken);
            return Ok(patient);
        }

        [HttpGet("{pageSize}/{pageNumber}")]
        [ProducesResponseType(typeof(PatientDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetPatients(int pageSize, int pageNumber, [FromQuery] PatientFiltrationModel filtrationModel, CancellationToken cancellationToken = default)
        {
            var patient = await _patientService.GetPatientsAsync(new Page(pageNumber, pageSize), filtrationModel, cancellationToken);
            return Ok(patient);
        }
    }
}
