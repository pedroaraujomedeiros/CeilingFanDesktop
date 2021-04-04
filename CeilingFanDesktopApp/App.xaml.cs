using Infra.Contexts;
using Infra.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CeilingFanDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var basePath = Directory.GetCurrentDirectory();
            var applicationPath = Directory.GetCurrentDirectory() + "/";


            var builder = new ConfigurationBuilder()
                 .SetBasePath(basePath)
                 .AddJsonFile("connectStrings.json", false, true)
                 .AddEnvironmentVariables();

            Configuration = builder.Build();

            
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

            mainWindow.Show();


        }

        private void ConfigureServices(IServiceCollection services)
        {
            //Using DbContext (EntityFrameWork)
            services.AddDbContext<FanDbContext>(
                options => options.UseSqlServer(Configuration["ConnectionStrings:CeilingFanConnStr"])
            );

            /* 
            => Dependency Injection
            - Transient
            - Scoped
            - Singleton
            */
            services.AddTransient<IUnitOfWork, UnitOfWork<FanDbContext>>();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddTransient(typeof(MainWindow));



        }
    }
}
