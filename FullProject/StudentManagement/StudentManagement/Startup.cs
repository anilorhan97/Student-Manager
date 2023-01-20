using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StudentManagement.DataModels;
using StudentManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {   //mypolicy isminiverdik. 
            services.AddCors(op => op.AddPolicy("MyPolicy", builder =>
            { //builder op kullanarak, bana gelen istek nerden gelsin sorusuna allowanyorigin ile her yerden gelsin diyoruz
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();  //Daha sonra allowa any method diyerek farketmez gibi.. En son Configure içinde UseCors tanýmlanýr.
            }));

           

            services.AddControllers();
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddDbContext<StudentAdminContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StudentAdminPortalDb"))); //ConfigureServices altýna service.Add.DbContext yazýldýktan sonra içine ilgili tablonun oluþacaðý context verilir.
            //Bu optionslar alýr. UseSqlServer() için gerekli using yapýlýr. Ýçine appsettings.json'da ki connectionstring key deðeri yazýlýr.
            //Entityframework.core kullanarak mgiration iþlemi yapýnca ne oluþturacaðýný belirtiyoruz. StudentAdminContext içindeki DbSet ile belirtilen tablolarý oluþturacak. Bunu nereye oluþturacak ?
            //Key'i StudentAdminPortalDb olanýn value'sine oluþturacak.
                        //Ýçerisine ilk interface daha sonra miras alaný yazarýz.
            services.AddScoped<IStudentRepository, SqlStudentRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentManagement", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentManagement v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
