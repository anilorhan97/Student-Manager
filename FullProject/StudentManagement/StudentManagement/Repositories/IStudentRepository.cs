using StudentManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.Repositories
{
    public interface IStudentRepository
    { //İlgili interfacede List ve içindeki Student Task'ın içine alınır. Sadece bu işlemle kalırsa SqlStudentRepository patlar
        //Patlamaması için ilgili yerde impelement işlemini yenilemeliyiz.
        Task<List<Student>> GetStudentsAsync();

        Task<Student> GetStudent(Guid studentId); //tek bir öğrenci old. için list kaldırıldı.

        Task<List<Gender>> GetGendersAsync();

        Task<bool> Exists(Guid studentId); //True false döndüreceği için bool yaptık. Ve parametre olarak öğrenciID'i alacaktı.

        Task<Student> UpdateStudent(Guid studentId, Student student); //request'i gönderiyoruz. Student student olacka.

        Task<Student> DeleteStudent(Guid studentId);

        Task<Student> AddStudent(Student student); //Mapleme işlemi yaparak domainModels'e çevireceğiz.
    }
}
