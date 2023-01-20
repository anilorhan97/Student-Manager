using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DataModels;
//using StudentManagement.DataModels;
using StudentManagement.DomainModels;
using StudentManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    [ApiController] //Bunun bir apicontroller olduğunu göstermemiz için başına bu eklenir.
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        public StudentController(IStudentRepository studentRepository, IMapper mapper) //constructor oluştrurduk. Ve daha sonra tanımladığımızı buraya da ekledim.
        {
            this.studentRepository = studentRepository; //ikinci studentRepositry constructor içerisindekii studentrepository'dir.
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        { //Ok restapi olduğu için kullanılır
            var students = await studentRepository.GetStudentsAsync();
            return Ok(mapper.Map<List<DomainModels.Student>>(students));
        }
        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetAllStudentAsync")] //bU KISIM ADD de işlev görmesi için eklendi.
        public async Task<IActionResult> GetAllStudentAsync([FromRoute] Guid studentId) //Routeden parametre aldığı için api'de fromRoute definition kullanılır.
        { 
            var student = await studentRepository.GetStudent(studentId);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DomainModels.Student>(student));
        }

        [HttpPut] //update işlemi olduğu için
        [Route("[controller]/{studentId:guid}")]

        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request) //Routeden parametre aldığı için api'de fromRoute definition kullanılır.
        {                                                                                                   //Ui tarafından gelen isteği requestte tutacağız.
        //StudentId gelecek.. Body'den de UpdateStudentRequest.cs'de ki istekleri request olarak tanımladık.
        //Eğer ki Exists ile o id'e ait biri varsa. UpdateStudent çalışacak ve id'i ve requestleri göndereceğiz.
        //Sonra içerisinde getStudent ile öyle bir öğrenci varsa (o id'e göre) çekilen öğrenci değişkenine atarız.
        //Ve dışarıdan,body'den gönderilen request bilgileri aktarılır ve kaydedilir.
            if(await studentRepository.Exists(studentId)) //Exists diye bir şey oluşturduk böyle bir şey varsa işlemlere devam et..
            {
                var updatedStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<DataModels.Student>(request));
                if(updatedStudent != null) //Doğru güncellendiyse
                {
                    return Ok(mapper.Map<DomainModels.Student>(updatedStudent));
                }
            }
            return NotFound();
        }

        
        [HttpDelete] 
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await studentRepository.Exists(studentId))
            {
                var student = await studentRepository.DeleteStudent(studentId);
                if (student != null) //Doğru güncellendiyse
                {
                    return Ok(mapper.Map<DomainModels.Student>(student));
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var student = await studentRepository.AddStudent(mapper.Map<DataModels.Student>(request)); //Çalışması için maplemeyi automapper ve updateStudentRquestAftermap gibi eklemeliyiz.
            return CreatedAtAction(nameof(GetAllStudentAsync), new { studentId = student.Id },mapper.Map<DomainModels.Student>(student));  //Verdiğimiz öğrencinin id'ini bize döner. Entityden dönen id'e eşit olacak.
        }

    }
}
