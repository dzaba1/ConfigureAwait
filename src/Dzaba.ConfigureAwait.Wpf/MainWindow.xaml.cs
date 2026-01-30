using Dzaba.ConfigureAwait.Lib;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace Dzaba.ConfigureAwait.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ILogger<MainWindow> logger;
    private readonly IMyService myService;

    public MainWindow(ILogger<MainWindow> logger, IMyService myService)
    {
        this.logger = logger;
        this.myService = myService;

        InitializeComponent();
    }

    private void SetLogs(int before, int after, ThreadIds serviceThreadIds)
    {
        try
        {
            LblLogs.Text = $"Thread IDs: Before call: {before}, Before delay: {serviceThreadIds.Before}, After delay: {serviceThreadIds.After}, After call: {after}";
        }
        catch (Exception ex)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                LblLogs.Text = ex.ToString();
            });
        }
    }

    private async void BtnA_Click(object sender, RoutedEventArgs e)
    {
        LblLogs.Text = string.Empty;

        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before service call: {ThreadId}", before);

        var result = await myService.SomeVeryImportantOperationAsync();

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after service call: {ThreadId}", after);

        SetLogs(before, after, result);
    }

    private async void BtnB_Click(object sender, RoutedEventArgs e)
    {
        LblLogs.Text = string.Empty;

        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before service call: {ThreadId}", before);

        var result = await myService.SomeVeryImportantOperationAsync().ConfigureAwait(false);

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after service call: {ThreadId}", after);

        SetLogs(before, after, result);
    }

    private async void BtnC_Click(object sender, RoutedEventArgs e)
    {
        LblLogs.Text = string.Empty;

        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before service call: {ThreadId}", before);

        var result = await myService.SomeVeryImportantOperationWithConfigureAwaitAsync();

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after service call: {ThreadId}", after);

        SetLogs(before, after, result);
    }

    private async void BtnD_Click(object sender, RoutedEventArgs e)
    {
        LblLogs.Text = string.Empty;

        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before service call: {ThreadId}", before);

        var result = await myService.SomeVeryImportantOperationWithConfigureAwaitAsync().ConfigureAwait(false);

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after service call: {ThreadId}", after);

        SetLogs(before, after, result);
    }

    private void BtnE_Click(object sender, RoutedEventArgs e)
    {
        LblLogs.Text = string.Empty;

        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before service call: {ThreadId}", before);

        var result = myService.SomeVeryImportantOperationWithConfigureAwaitAsync().Result;

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after service call: {ThreadId}", after);

        SetLogs(before, after, result);
    }

    private void BtnF_Click(object sender, RoutedEventArgs e)
    {
        LblLogs.Text = string.Empty;

        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before service call: {ThreadId}", before);

        var result = myService.SomeVeryImportantOperationAsync().Result;

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after service call: {ThreadId}", after);

        SetLogs(before, after, result);
    }
}