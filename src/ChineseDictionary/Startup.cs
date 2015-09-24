using ChineseDictionary.Resources;
using ChineseDictionary.Resources.Managers;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

namespace ChineseDictionary
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public IApplicationEnvironment ApplicationEnvironment { get; set; }
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var config = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            Configuration = config.Build();
            ApplicationEnvironment = appEnv;
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();           

            services.AddEntityFramework()
                .AddSqlite()
                .AddDbContext<DictionaryContext>(c =>
                {
                    c.UseSqlite($"Data Source={ApplicationEnvironment.ApplicationBasePath}/{Configuration["Data:ConnectionStrings:Sqlite"]};");
                });

            services.AddTransient<ICharacterManager, CharacterManager>(collection => new CharacterManager(
                collection
                    .GetService
                    <DictionaryContext>()));

            services.AddTransient<IIdiomManager, IdiomManager>(collection =>
                new IdiomManager(collection.GetService<DictionaryContext>(), collection.GetService<ICharacterManager>()));

            services.AddTransient<IPhraseManager, PhraseManager>(collection =>
                new PhraseManager(collection.GetService<DictionaryContext>(), collection.GetService<ICharacterManager>()));

        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();

            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc(c =>
            {
                c.MapRoute("Default", "api/{controller}/{action}/{id?}");
                c.MapRoute("Angular", "{*anything}", new
                {
                    controller = "Default",
                    action = "Index"
                });
            });
        }
    }
}
