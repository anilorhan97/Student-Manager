using Microsoft.EntityFrameworkCore;
using StudentManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StudentManagement.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;
        public SqlStudentRepository(StudentAdminContext context) //constructor açıp context'i içine atarız.
        {
            this.context = context; //Birbirine eşitledikten sonra artık db'ye erişim sağlayabiliriz.
        }

        public async Task<List<Student>> GetStudentsAsync()
        { //Nasıl çekeriz ? Oluşturduğumuz Context ile çekeriz. Yeni bir ctor ile cons açıp ilgili Context'i verip adlandırırız.
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }


        public async Task<Student> GetStudent(Guid studentId) //tipi guid'di.
        {                   //id'i studentId'den gönderdiğim değere eşit olacak..
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(x => x.Id == studentId);
        }//Eğer varsa direkt onu al yoksa boş gönder.. Artık controller'e gönderebiliriz.

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<bool> Exists(Guid studentId)
        {
            return await context.Student.AnyAsync(x=> x.Id == studentId); //Any ile içerisinde studentId'e sahip bir şey var mı diye kontrol ediliyor. T-f döner
        }

        public async Task<Student> UpdateStudent(Guid studentId, Student request) 
        {
            var existingStudent = await GetStudent(studentId); //Tek bir öğrenci çekeceğimiz için üstteki bu metod kullanıldı. Ve bunu bir değişkene attık.
            if(existingStudent != null)
            {
                existingStudent.firstName = request.firstName;
                existingStudent.lastName = request.lastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.email = request.email;
                existingStudent.mobile = request.mobile;
                existingStudent.genderId = request.genderId;
                existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;

                await context.SaveChangesAsync(); //Kayıt etmek için db'ye..
                return existingStudent;
            }
            return null;
        }

        public async Task<Student> DeleteStudent(Guid studentId)
        {
            var existingStudent = await GetStudent(studentId); //Tek bir öğrenci çekeceğimiz için üstteki bu metod kullanıldı. Ve bunu bir değişkene attık.
            if (existingStudent != null)
            {
                context.Student.Remove(existingStudent);

                await context.SaveChangesAsync(); //Kayıt etmek için db'ye..
                return existingStudent;
            }
            return null;
        }

        public async Task<Student> AddStudent(Student request)
        {
            var newStudent = await context.Student.AddAsync(request);
            await context.SaveChangesAsync();
            return newStudent.Entity; //Eklediğimiz newStudent'nin geri dönüşünü .entity ile alabiliriz.
        }
    }
}
