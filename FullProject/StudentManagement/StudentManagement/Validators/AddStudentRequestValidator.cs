using FluentValidation;
using StudentManagement.DomainModels;
using StudentManagement.Repositories;
using System.Linq;

namespace StudentManagement.Validators
{
    public class AddStudentRequestValidator : AbstractValidator<AddStudentRequest> //Kütüphaneler eklenmeli
    {
        public AddStudentRequestValidator(IStudentRepository studentRepository)
        {
            RuleFor(x => x.firstName).NotEmpty();
            RuleFor(x => x.lastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.email).NotEmpty().EmailAddress(); //Email için ayrı formatı gereği valid kuralı eklendi.
            RuleFor(x => x.mobile).GreaterThan(99999).LessThan(10000000000);
            RuleFor(x => x.genderId).NotEmpty().Must(id =>
            {
                var gender = studentRepository.GetGendersAsync().Result.ToList().FirstOrDefault(x => x.Id == id);
                //studentRep'den GetGender'i Liste halinde çekiyoruz. Ve gönderdiğimiz id gender içinde bulunuyorsa ilkini çeker ve true döner..
                if (gender != null)
                {
                    return true;
                }
                return false;
            }).WithMessage("Please select a valid Gender");
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();

        }
    }
}
