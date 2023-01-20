using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DomainModels;
using StudentManagement.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        public GendersController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository; //ikinci studentRepositry constructor içerisindekii studentrepository'dir.
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGendersAsync()
        { 
            var genderList = await studentRepository.GetGendersAsync();
            if(genderList == null || !genderList.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<Gender>>(genderList));
        }   
    }
}
