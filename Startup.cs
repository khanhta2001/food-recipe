using FoodRecipe.Services;
using MongoDB.Driver;

namespace FoodRecipe
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var connectionUrl = config.GetSection("MongoDB:ConnectionUrl").Value;
                return new MongoClient(connectionUrl);
            });
            services.AddScoped<IMongoDatabase>(serviceProvider =>
            {
                var mongoClient = serviceProvider.GetRequiredService<IMongoClient>();
                IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var databaseName = config.GetSection("MongoDB:DatabaseName").Value;
                return mongoClient.GetDatabase(databaseName);
            });
            services.AddScoped<DataService>();
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
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

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=HomePage}/{id?}");

            app.Run();
        }
    }   
}