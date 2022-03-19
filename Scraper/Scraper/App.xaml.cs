using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Scraper.Domain.Interfaces;
using Scraper.Domain.Services;
using Serilog;
using System;
using System.Windows;
using System.Windows.Input;

namespace Scraper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private ServiceProvider serviceProvider;
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .UseSerilog((host, loggerConfiguration) =>
                {
                    loggerConfiguration.WriteTo.File("test.txt", rollingInterval: RollingInterval.Day)
                        .WriteTo.Debug()
                        .MinimumLevel.Error()
                        .MinimumLevel.Override("Scraper.Project", Serilog.Events.LogEventLevel.Debug);
                })
                .ConfigureServices(DoConfigure)
                .Build();
        }

        private void DoConfigure(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddScoped<IScraperService, ScraperService>();
            //Serilog.Logger
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
