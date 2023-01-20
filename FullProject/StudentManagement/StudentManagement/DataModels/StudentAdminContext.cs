using Microsoft.EntityFrameworkCore;

namespace StudentManagement.DataModels
{
    public class StudentAdminContext : DbContext
    {   //DbContextOptions'a oluşturduğumuz StudentAdminContext'i veriyoruz. Daha sonra option diyerek neyi base aldığını belirtiriz. Optionsları base alıyoruz.
        public StudentAdminContext(DbContextOptions<StudentAdminContext> options) : base(options)
        {}
        public DbSet<Student> Student { get; set; } //Student içindeki proporty'lerin ör ad her birinin kolon olarak gözükmesi isteniyor.
        //İkinci kısma Sql'de tablonun hangi isimde gözükmesini istediğimizi belirtiyoruz.
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
