using System;

namespace StudentManagement.DomainModels
{
    public class AddStudentRequest
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string DateOfBirth { get; set; }
        public string email { get; set; }
        public long mobile { get; set; }

        public Guid genderId { get; set; }

        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }
    }
}
