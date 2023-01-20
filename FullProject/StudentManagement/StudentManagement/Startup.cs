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
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();  //Daha sonra allowa any method diyerek farketmez gibi.. En son Configure i�inde UseCors tan�mlan�r.
            }));

           

            services.AddControllers();
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddDbContext<StudentAdminContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StudentAdminPortalDb"))); //ConfigureServices alt�na service.Add.DbContext yaz�ld�ktan sonra i�ine ilgili tablonun olu�aca�� context verilir.
            //Bu optionslar al�r. UseSqlServer() i�in gerekli using yap�l�r. ��ine appsettings.json'da ki connectionstring key de�eri yaz�l�r.
            //Entityframework.core kullanarak mgiration i�lemi yap�nca ne olu�turaca��n� belirtiyoruz. StudentAdminContext i�indeki DbSet ile belirtilen tablolar� olu�turacak. Bunu nereye olu�turacak ?
            //Key'i StudentAdminPortalDb olan�n value'sine olu�turacak.
                        //��erisine ilk interface daha sonra miras alan� yazar�z.
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
