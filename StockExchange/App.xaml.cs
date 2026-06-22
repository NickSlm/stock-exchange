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
namespace StockExchange;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    public static IServiceProvider Services { get; set; }
    private CancellationTokenSource _cts = new CancellationTokenSource();

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var configuration = new ConfigurationBuilder().
            SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true)
            .Build();


        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IConfiguration>(configuration);
        ConfigureServices(serviceCollection, configuration);
        Services = serviceCollection.BuildServiceProvider();

        var vm = Services.GetRequiredService<MainWindowViewModel>();
        await vm.LoadAsync();

        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Show();


        var tradeSim = Services.GetRequiredService<ITradeSimService>();
        _ = tradeSim.StartAsync(_cts.Token);

    }


    protected override void OnExit(ExitEventArgs e)
    {
        _cts.Cancel();
        base.OnExit(e);
    }
    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<MainWindow>();
        services.AddDbContext<ExchangeContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);
        services.AddSingleton<ITradeSimService,TradeSimService>();
        services.AddSingleton<IDbService, DbService>();

        services.AddSingleton<MainWindowViewModel>();
    }


}

