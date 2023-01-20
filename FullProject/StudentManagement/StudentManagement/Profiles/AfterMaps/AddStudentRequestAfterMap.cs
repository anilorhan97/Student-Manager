using AutoMapper;
using StudentManagement.DomainModels;
using System;

namespace StudentManagement.Profiles.AfterMaps
{
    public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, DataModels.Student>
    {
        public void Process(AddStudentRequest source, DataModels.Student destination, ResolutionContext context) //Source kısmı UpdateStudentRequest. Çevireceğimiz kısım Student kısmı.
        {
            //id'i bilmediğimiz için adres eklerken onu da belirtmemiz lazım
            destination.Id = Guid.NewGuid(); //random guild değeri atadık.
            destination.Address = new DataModels.Address()
            {
                Id = Guid.NewGuid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
