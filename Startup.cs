using EventApp.Context;
using EventApp.Implementations.Repositories;
using EventApp.Implementations.Services;
using EventApp.Interfaces.Repositories;
using EventApp.Interfaces.Services;
using EventApp.Services;
using EventApp.Setting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(Options => Options.UseMySQL(Configuration.GetConnectionString("ConnectionContext")));
            services.AddControllersWithViews();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailServices, MailService>();
            
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAttendeeRepository, AttendeeRepository>();
            services.AddScoped<IAttendeeService, AttendeeService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IOrganizerRepository, OrganizerRepository>();
            services.AddScoped<IOrganizerService, OrganizerService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserRepository, UserRepository> ();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEventAttendeeRepository, EventAttendeeRepository>();
            

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config => 
                {
                    config.LoginPath = "/user/login";
                    config.Cookie.Name = "EventApp";
                    config.LogoutPath = "/user/logout";
                });
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
