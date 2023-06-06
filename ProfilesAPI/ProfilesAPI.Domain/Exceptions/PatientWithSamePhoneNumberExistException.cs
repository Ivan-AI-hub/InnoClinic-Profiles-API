namespace ProfilesAPI.Domain.Exceptions
{
    public class PatientWithSamePhoneNumberExistException : BadRequestException
    {
        public PatientWithSamePhoneNumberExistException(string phoneNumber) 
            : base($"Patient with phoneNumber={phoneNumber} already exist")
        {
        }
    }
}
