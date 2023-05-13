﻿using Microsoft.AspNetCore.Http;

namespace ProfilesAPI.Services.Abstraction.AggregatesModels.DoctorAggregate
{
    public record CreateDoctorModel(IFormFile? Photo,
                                     string Email,
                                     string FirstName,
                                     string LastName,
                                     string MiddleName,
                                     DateOnly BirthDay,
                                     string Specialization,
                                     Guid OfficeId,
                                     int CareerStartYear);
}
