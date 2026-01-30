using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;
using Dzaba.ConfigureAwait.Lib;

namespace Dzaba.ConfigureAwait.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider container;

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var services = new ServiceCollection();
        services.RegisterConfigureAwaitLib();  

        var logger = new LoggerConfiguration()
            .Enrich.WithThreadId()
            .WriteTo.Debug(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{ThreadId}] [{SourceContext}] {Level:u3} - {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        services.AddLogging(l => l.AddSerilog(logger, true));

        services.AddSingleton<MainWindow>();

        container = services.BuildServiceProvider();

        var mainWindow = container.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        container?.Dispose();
    }

    private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show($"An unhandled exception occurred: {e.Exception}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}