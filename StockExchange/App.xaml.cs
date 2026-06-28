using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using DataLayer.Context;
using BusinessLayer;
using DataLayer.Service;
using DataLayer.Interface;
using Microsoft.EntityFrameworkCore;
using StockExchange.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
namespace StockExchange;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    public static IServiceProvider Services { get; set; }
    private IHost _host;
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Dispatcher.InvokeAsync(InitializeAsync);

    }

    private async Task InitializeAsync()
    {
        try
        {
            var host = Host.CreateDefaultBuilder()
             .ConfigureAppConfiguration((context, config) =>
                 {
                     config.SetBasePath(AppContext.BaseDirectory);
                     config.AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true);
                 })
                 .ConfigureServices((context, services) =>
                 {
                     ConfigureServices(services, context.Configuration);
                 })
                 .Build();
            _host = host;
            Services = host.Services;
            var mainWindow = Services.GetRequiredService<MainWindow>();
            await host.StartAsync();
            mainWindow.Show();

        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed To start: {ex.Message}");
            Shutdown();
        }

    }
    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        base.OnExit(e);

    }
    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<MainWindow>();
        services.AddDbContext<ExchangeContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddHostedService<TradeSimService>();
        services.AddSingleton<ICurrencyStorage,CurrencyStorage>();
        services.AddSingleton<IDbService, DbService>();

        services.AddSingleton<MainWindowViewModel>();
    }


}

