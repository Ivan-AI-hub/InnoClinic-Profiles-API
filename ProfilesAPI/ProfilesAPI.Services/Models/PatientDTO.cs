namespace ProfilesAPI.Services.Models
{
    public class PatientDTO
    {
        public Guid Id { get; set; }
        public HumanInfoDTO Info { get; set; }
        public string PhoneNumber { get; set; }

        public PatientDTO(Guid id, HumanInfoDTO info, string phoneNumber)
        {
            Id = id;
            Info = info;
            PhoneNumber = phoneNumber;
        }
    }
}
