using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyNotes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


// help from: https://metanit.com/sharp/aspnet5/16.2.php

namespace MyNotes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //В методе ConfigureServices() добавляются сервисы для Entity Framework Core:
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Далее добавляются специфичные для Identity сервисы:
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            /*
             * Метод AddIdentity() позволяет установить некоторую начальную конфигурацию. 
             * Здесь мы указываем тип пользователя и тип роли, которые будут использоваться системой Identity. 
             * В качестве типа пользователя выступает созданный нами выше класс User, а в качестве типа роли взят стандартный класс IdentityRole, 
             * который находится в пространстве имен Microsoft.AspNetCore.Identity.EntityFrameworkCore.
                        
            Метод AddEntityFrameworkStores() устанавливает тип хранилища, которое будет применяться в Identity для хранения данных. 
            В качестве типа хранилища здесь указывается класс контекста данных.
            */

            services.AddMvc();
            
        }

        /*
         Затем, чтобы использовать Identity, в методе Configure() устанавливается компонент middeware - UseAuthentication. 
         Причем это middleware вызывается перед UseMvc(), тем самым гарантируя, что ко времени обращения к системе маршрутизации,
         контроллерам и их методам, куки должным образом обработаны и установлены.
             */
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
