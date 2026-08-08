using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SnazzySpinTheWheel.Services;
using SnazzySpinTheWheel.ViewModels;

namespace SnazzySpinTheWheel;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        ConfigureServices(services);
        Services = services.BuildServiceProvider();

        var window = Services.GetRequiredService<MainWindow>();
        MainWindow = window;
        window.Show();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IRandomService, RandomService>();
        services.AddSingleton<IWheelFactory, WheelFactory>();
        services.AddSingleton<IWheelSpinService, WheelSpinService>();
        services.AddSingleton<SpinnerViewModel>();
        services.AddSingleton<MainWindow>();
    }
}
