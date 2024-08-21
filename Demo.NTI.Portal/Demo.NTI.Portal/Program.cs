using Demo.NTI.BLL.MyMapper;
using Demo.NTI.BLL.Repository;
using Demo.NTI.BLL.Service;
using Demo.NTI.DAL.Database;
using Demo.NTI.DAL.Extend;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.NTI.Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Generic Repository Service

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // new GenericRepository<Type>();

            #endregion

            #region Application Services

            builder.Services.AddScoped<IDepartmentService, DepartmentService>(); // new DepartmentService()
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<ICityService, CityService>();

            #endregion

            #region Auto Mapper

            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            #endregion

            #region Connection String

            // Enhancement ConnectionString
            var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");

            builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));

            #endregion


            #region Microsoft Identity



            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });



            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                                                       .AddRoles<ApplicationRole>()
                                                       .AddEntityFrameworkStores<ApplicationContext>()
                                                       .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);





            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Default Password settings.
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequiredLength = 6;
                //options.Password.RequiredUniqueChars = 0;

                //Options For USer
                //options.User.RequireUniqueEmail = true;
                //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";


            }).AddEntityFrameworkStores<ApplicationContext>();


            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
