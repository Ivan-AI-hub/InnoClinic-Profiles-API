﻿namespace ProfilesAPI.Domain
{
    public class HumanInfo
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Picture? Photo { get; set; }
        public string Email { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDay { get; set; }
        private HumanInfo() { }
        public HumanInfo(string email, string firstName, string lastName, string middleName, DateTime birthDay)
        {
            Photo = null;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthDay = birthDay;
        }
    }
}
