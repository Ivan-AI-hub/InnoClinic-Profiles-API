﻿using AutoMapper;
using FluentValidation;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels;
using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Application.Abstraction.QueryableManipulation;
using ProfilesAPI.Application.Filtrators;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Application
{
    public class DoctorService : BaseService, IDoctorService
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

            if (model.Info.Photo != null)
            {
                await _blobService.UploadAsync(model.Info.Photo);
            }

            return await GetDoctorDTOWithPhotoAsync(doctor, cancellationToken);
        }

        public async Task EditDoctorAsync(Guid id, EditDoctorModel model, CancellationToken cancellationToken = default)
        {
            await ValidateBlobFileName(model.Photo, cancellationToken);
            await ValidateModel(model, _editDoctorModel, cancellationToken);

            var oldDoctor = await _repositoryManager.DoctorRepository.GetItemAsync(id, cancellationToken);
            if (oldDoctor == null)
            {
                throw new DoctorNotFoundException(id);
            }

            var doctor = _mapper.Map<Doctor>(model);

            await _repositoryManager.DoctorRepository.UpdateAsync(id, doctor);

            if (oldDoctor.Info.Photo != null)
            {
                await _blobService.DeleteAsync(oldDoctor.Info.Photo.Name);
            }

            if (model.Photo != null)
            {
                await _blobService.UploadAsync(model.Photo);
            }
        }

        public async Task EditDoctorStatusAsync(Guid id, WorkStatusDTO workStatus, CancellationToken cancellationToken = default)
        {
            await _repositoryManager.DoctorRepository.UpdateStatusAsync(id, _mapper.Map<WorkStatus>(workStatus));
        }

        public async Task<DoctorDTO> GetDoctorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctor = await _repositoryManager.DoctorRepository.GetItemAsync(id, cancellationToken);
            if (doctor == null)
            {
                throw new DoctorNotFoundException(id);
            }

            return await GetDoctorDTOWithPhotoAsync(doctor, cancellationToken);
        }

        public IEnumerable<DoctorDTO> GetDoctors(Page page, DoctorFiltrationModel filtrationModel)
        {
            return GetFiltratedDoctors(page, filtrationModel).ToList().Select(x => GetDoctorDTOWithPhotoAsync(x).Result);
        }

        public IEnumerable<DoctorDTO> GetDoctorsInfo(Page page, DoctorFiltrationModel filtrationModel)
        {
            var doctors = GetFiltratedDoctors(page, filtrationModel);

            return _mapper.Map<IEnumerable<DoctorDTO>>(doctors);
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

        private IQueryable<Doctor> GetFiltratedDoctors(Page page, DoctorFiltrationModel filtrationModel)
        {
            var doctors = _repositoryManager.DoctorRepository.GetItems();
            var filtrator = _mapper.Map<IFiltrator<Doctor>>(filtrationModel);
            doctors = filtrator.Filtrate(doctors);
            doctors = PageSeparator.GetPage(doctors, page);
            return doctors;
        }
    }
}
