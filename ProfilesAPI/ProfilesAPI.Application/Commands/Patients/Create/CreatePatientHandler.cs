using AutoMapper;
using FluentValidation;
using MediatR;
using ProfilesAPI.Application.Results;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Application.Commands.Patients.Create
{
    public class CreatePatientHandler : IRequestHandler<CreatePatient, ApplicationValueResult<Patient>>
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;
        private IValidator<CreatePatient> _validator;
        public CreatePatientHandler(IRepositoryManager repositoryManager,IMapper mapper, IValidator<CreatePatient> validator)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<ApplicationValueResult<Patient>> Handle(CreatePatient request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) 
            {
                return new ApplicationValueResult<Patient>(validationResult);
            }
            var patient = _mapper.Map<Patient>(request);

            await _repositoryManager.PatientRepository.CreateAsync(patient);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
            return new ApplicationValueResult<Patient>(patient);
        }
    }
}
