using JO_MOVIES.Data.Enum;
using Microsoft.EntityFrameworkCore;
using JO_MOVIES.Data;
using JO_MOVIES.Data.service;
using JO_MOVIES.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace JO_MOVIES
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
			// DbContext configuration
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("JO-MOVIES")));
			// Add service configurations
			services.AddScoped<IActorService,ActorsService>();
            services.AddScoped<IProducersService, ProducersService>();
            services.AddScoped<ICinemasService, CinemasService>();
			services.AddScoped<IMovieService, MoviesService>();
            services.AddScoped<ITicketsService, TicketsService>();
            ;

           


            //Authentication and authorization
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            services.AddControllersWithViews();

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
            app.UseSession();

            //Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // User area route
                endpoints.MapControllerRoute(
                    name: "user_default",
                    pattern: "{controller=Tickets}/{action=Index}/{id?}",
                    defaults: new { area = "User" });

                // Admin area route
                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "admin/{controller=Tickets}/{action=Index}/{id?}",
                    defaults: new { area = "Admin" });


            });

            //Seed database
            AppDbInitializer.Seed(app);
           // AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
        }


    }
}
