﻿using Microsoft.AspNetCore.Mvc;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Presentation.Models.ErrorModels;

namespace ProfilesAPI.Presentation.Controllers
{
    [ApiController]
    [Route("/doctors/")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DoctorDTO), 201)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> CreateAsync(CreateDoctorModel createModel, CancellationToken cancellationToken = default)
        {
            var doctor = await _doctorService.CreateDoctorAsync(createModel, cancellationToken);
            return CreatedAtAction(nameof(GetDoctorAsync), new { id = doctor.Id }, doctor);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> EditAsync(Guid id, EditDoctorModel editModel, CancellationToken cancellationToken = default)
        {
            await _doctorService.EditDoctorAsync(id, editModel, cancellationToken);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> EditStatusAsync(Guid id, WorkStatusDTO workStatus, CancellationToken cancellationToken = default)
        {
            await _doctorService.EditDoctorStatusAsync(id, workStatus, cancellationToken);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DoctorDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetDoctorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctor = await _doctorService.GetDoctorAsync(id, cancellationToken);
            return Ok(doctor);
        }

        [HttpGet("{pageSize}/{pageNumber}")]
        [ProducesResponseType(typeof(DoctorDTO), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetDoctorsAsync(int pageSize, int pageNumber, [FromQuery] DoctorFiltrationModel filtrationModel, CancellationToken cancellationToken = default)
        {
            var doctors = await _doctorService.GetDoctorsAsync(new Page(pageNumber, pageSize), filtrationModel, cancellationToken);
            return Ok(doctors);
        }
    }
}
