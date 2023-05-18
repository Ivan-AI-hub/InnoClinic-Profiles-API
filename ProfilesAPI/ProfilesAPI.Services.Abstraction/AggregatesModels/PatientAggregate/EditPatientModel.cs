﻿using Microsoft.AspNetCore.Http;

namespace ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate
{
    public record EditPatientModel(IFormFile? Photo,
                                     string FirstName,
                                     string LastName,
                                     string MiddleName,
                                     DateTime BirthDay,
                                     string PhoneNumber);
}
