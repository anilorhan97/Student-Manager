using AutoMapper;
using StudentManagement.DataModels;
using StudentManagement.DomainModels;
using StudentManagement.Profiles.AfterMaps;
using DataModels = StudentManagement.DataModels; 

namespace StudentManagement.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {   //İlki datamodel ikinci domainmodel
            CreateMap<DataModels.Student, DomainModels.Student>().ReverseMap();
            CreateMap<DataModels.Gender, DomainModels.Gender>().ReverseMap();
            CreateMap<DataModels.Address, DomainModels.Address>().ReverseMap();
            CreateMap<UpdateStudentRequest, DataModels.Student>().AfterMap<UpdateStudentRequestAfterMap>();
            CreateMap<AddStudentRequest, DataModels.Student>().AfterMap<AddStudentRequestAfterMap>();

        }
    }
}
