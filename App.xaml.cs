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
        ConfigureServices(services, numSlices: 10);
        Services = services.BuildServiceProvider();

        var window = Services.GetRequiredService<MainWindow>();
        MainWindow = window;
        window.Show();
    }

    private static void ConfigureServices(IServiceCollection services, int numSlices)
    {
        services.AddSingleton<IRandomService, RandomService>();
        services.AddSingleton<IWheelFactory>(provider => 
            new WheelFactory(numSlices, provider.GetRequiredService<IRandomService>()));
        services.AddSingleton<IWheelSpinService, WheelSpinService>();
        services.AddSingleton<SpinnerViewModel>();
        services.AddSingleton<MainWindow>();
    }
}
