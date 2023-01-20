using System;

namespace StudentManagement.DataModels
{
    public class Student
    {
        public static Student Entity { get; internal set; }
        public Guid Id { get; set; } //unic 
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string profileImageUrl { get; set; }
        public Guid genderId { get; set; }
        //Gender class tablosuyla ilişkisi olacak. Class'ı oluşturulunca kırmızılık gider.
        public Gender Gender { get; set; } 
        public Address Address { get; set; }
    }
}
