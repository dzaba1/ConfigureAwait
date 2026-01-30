using Microsoft.Extensions.Logging;

namespace Dzaba.ConfigureAwait.Lib;

public interface IMyService
{
    Task<ThreadIds> SomeVeryImportantOperationAsync();
    Task<ThreadIds> SomeVeryImportantOperationWithConfigureAwaitAsync();
}

internal sealed class MyService : IMyService
{
    private readonly ILogger<MyService> logger;

    public MyService(ILogger<MyService> logger)
    {
        this.logger = logger;
    }

    public async Task<ThreadIds> SomeVeryImportantOperationAsync()
    {
        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before delay: {ThreadId}", before);
        
        await Task.Delay(3000);
        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after delay: {ThreadId}", after);

        return new ThreadIds(before, after);
    }

    public async Task<ThreadIds> SomeVeryImportantOperationWithConfigureAwaitAsync()
    {
        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before delay: {ThreadId}", before);

        await Task.Delay(3000).ConfigureAwait(false);

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after delay: {ThreadId}", after);

        return new ThreadIds(before, after);
    }
}