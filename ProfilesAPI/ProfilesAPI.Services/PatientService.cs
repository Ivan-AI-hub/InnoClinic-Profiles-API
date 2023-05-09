using AutoMapper;
using MediatR;
using ProfilesAPI.Application.Commands.Patients.Create;
using ProfilesAPI.Domain;
using ProfilesAPI.Services.Models;
using ProfilesAPI.Services.Results;

namespace ProfilesAPI.Services
{
    public class PatientService
    {
        private IMediator _mediator;
        private IMapper _mapper;
        private BlobService _blobService;
        public PatientService(IMediator mediator, IMapper mapper, BlobService blobService) 
        {
            _mediator = mediator;
            _mapper = mapper;
            _blobService = blobService;
        }

        public async Task<ServiceValueResult<PatientDTO>> CreatePatientAsync(CreatePatientModel model, CancellationToken cancellationToken = default)
        {
            if (model.Photo != null)
            {
                var blobFileResult = await IsBlobFileNameValid(model.Photo.FileName);
                if (!blobFileResult.IsComplite)
                    return new ServiceValueResult<PatientDTO>(blobFileResult);
            }

            var request = _mapper.Map<CreatePatient>(model);
            var applicationResult = await _mediator.Send(request, cancellationToken);

            if (!applicationResult.IsComplite)
                return new ServiceValueResult<PatientDTO>(applicationResult);

            if (model.Photo != null)
                await _blobService.UploadAsync(model.Photo);

            var patient = await GetPatientDTOWithPhotoAsync(applicationResult.Value);
            return new ServiceValueResult<PatientDTO>(patient);
        }

        private async Task<PatientDTO> GetPatientDTOWithPhotoAsync(Patient patientData, CancellationToken cancellationToken = default)
        {
            var patient = _mapper.Map<PatientDTO>(patientData);

            if (patientData.Info.Photo != null)
            {
                patient.Info.Photo = await _blobService.DownloadAsync(patientData.Info.Photo.Name, cancellationToken);
            }

            return patient;
        }

        private async Task<IServiceResult> IsBlobFileNameValid(string blobFileName)
        {
            if (await _blobService.IsBlobExist(blobFileName))
            {
                return new ServiceVoidResult(errors: "File with the same name already exist in database");
            }
            return new ServiceVoidResult();
        }
    }
}
