using AutoMapper;
using FluentValidation;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;
using ProfilesAPI.Services.Abstraction;
using ProfilesAPI.Services.Abstraction.AggregatesModels;
using ProfilesAPI.Services.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Services.Abstraction.QueryableManipulation;
using ProfilesAPI.Services.Filtrators;

namespace ProfilesAPI.Services
{
    internal class DoctorService : BaseService, IDoctorService
    {
        private IMapper _mapper;
        private IValidator<CreateDoctorModel> _createDoctorValidator;
        private IValidator<EditDoctorModel> _editDoctorModel;

        public DoctorService(IRepositoryManager repositoryManager, IBlobService blobService, IMapper mapper,
            IValidator<CreateDoctorModel> createDoctorValidator, IValidator<EditDoctorModel> editDoctorModel) : base(blobService, repositoryManager)
        {
            _mapper = mapper;
            _createDoctorValidator = createDoctorValidator;
            _editDoctorModel = editDoctorModel;
            _blobService = blobService;
        }
        public async Task<DoctorDTO> CreateDoctorAsync(CreateDoctorModel model, CancellationToken cancellationToken = default)
        {
            await ValidateBlobFileName(model.Info.Photo, cancellationToken);
            await ValidateModel(model, _createDoctorValidator, cancellationToken);
            await ValidateEmailAsync(model.Info.Email, cancellationToken);

            var doctor = _mapper.Map<Doctor>(model);

            await _repositoryManager.DoctorRepository.CreateAsync(doctor);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            if (model.Info.Photo != null)
                await _blobService.UploadAsync(model.Info.Photo);

            return await GetDoctorDTOWithPhotoAsync(doctor, cancellationToken);
        }

        public async Task<DoctorDTO> EditDoctorAsync(Guid id, EditDoctorModel model, CancellationToken cancellationToken = default)
        {
            await ValidateBlobFileName(model.Photo, cancellationToken);
            await ValidateModel(model, _editDoctorModel, cancellationToken);

            var oldDoctor = await _repositoryManager.DoctorRepository.GetItemAsync(id, false, cancellationToken);
            if (oldDoctor == null)
                throw new DoctorNotFoundException(id);

            var doctor = _mapper.Map<Doctor>(model);

            await _repositoryManager.DoctorRepository.UpdateAsync(id, doctor);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            if (oldDoctor.Info.Photo != null)
                await _blobService.DeleteAsync(oldDoctor.Info.Photo.Name);

            if (model.Photo != null)
                await _blobService.UploadAsync(model.Photo);

            return await GetDoctorDTOWithPhotoAsync(doctor, cancellationToken);
        }

        public async Task EditDoctorStatusAsync(Guid id, WorkStatusDTO workStatus, CancellationToken cancellationToken = default)
        {
            var oldDoctor = await _repositoryManager.DoctorRepository.GetItemAsync(id, true, cancellationToken);
            if (oldDoctor == null)
                throw new DoctorNotFoundException(id);

            oldDoctor.Status = _mapper.Map<WorkStatus>(workStatus);
            await _repositoryManager.SaveChangesAsync();
        }

        public async Task<DoctorDTO> GetDoctorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctor = await _repositoryManager.DoctorRepository.GetItemAsync(id, false, cancellationToken);
            if (doctor == null)
                throw new DoctorNotFoundException(id);

            return await GetDoctorDTOWithPhotoAsync(doctor, cancellationToken);
        }

        public IEnumerable<DoctorDTO> GetDoctors(Page page, DoctorFiltrationModel filtrationModel)
        {
            var doctors = _repositoryManager.DoctorRepository.GetItems(false);
            var filtrator = _mapper.Map<IFiltrator<Doctor>>(filtrationModel);
            doctors = filtrator.Filtrate(doctors);
            doctors = PageSeparator.GetPage(doctors, page);

            return doctors.ToList().Select(x => GetDoctorDTOWithPhotoAsync(x).Result);
        }

        private async Task<DoctorDTO> GetDoctorDTOWithPhotoAsync(Doctor doctorData, CancellationToken cancellationToken = default)
        {
            var doctor = _mapper.Map<DoctorDTO>(doctorData);

            if (doctorData.Info.Photo != null)
            {
                doctor.Info.Photo = await _blobService.DownloadAsync(doctorData.Info.Photo.Name, cancellationToken);
            }

            return doctor;
        }
    }
}
