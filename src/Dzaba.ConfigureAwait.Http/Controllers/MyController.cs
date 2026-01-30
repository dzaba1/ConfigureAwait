using Dzaba.ConfigureAwait.Lib;
using Microsoft.AspNetCore.Mvc;

namespace Dzaba.ConfigureAwait.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class MyController : ControllerBase
{
    private readonly ILogger<MyController> logger;
    private readonly IMyService myService;

    public MyController(ILogger<MyController> logger,
        IMyService myService)
    {
        this.logger = logger;
        this.myService = myService;        
    }

    [HttpGet("a")]
    public async Task<AllThreadIds> GetWithoutConfigureAwaitWithoutConfigureAwait()
    {
        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before service call: {ThreadId}", before);

        var result =  await myService.SomeVeryImportantOperationAsync();

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after service call: {ThreadId}", after);

        return new AllThreadIds(before, after, result.Before, result.After);
    }

    [HttpGet("b")]
    public async Task<AllThreadIds> GetWithoutConfigureAwaitWithConfigureAwait()
    {
        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before service call: {ThreadId}", before);

        var result = await myService.SomeVeryImportantOperationAsync().ConfigureAwait(false);

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after service call: {ThreadId}", after);

        return new AllThreadIds(before, after, result.Before, result.After);
    }

    [HttpGet("c")]
    public async Task<AllThreadIds> GetWithConfigureAwaitWithoutConfigureAwait()
    {
        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before service call: {ThreadId}", before);

        var result = await myService.SomeVeryImportantOperationWithConfigureAwaitAsync();

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after service call: {ThreadId}", after);

        return new AllThreadIds(before, after, result.Before, result.After);
    }

    [HttpGet("d")]
    public async Task<AllThreadIds> GetWithConfigureAwaitWithConfigureAwait()
    {
        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before service call: {ThreadId}", before);

        var result = await myService.SomeVeryImportantOperationWithConfigureAwaitAsync().ConfigureAwait(false);

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after service call: {ThreadId}", after);

        return new AllThreadIds(before, after, result.Before, result.After);
    }

    [HttpGet("e")]
    public AllThreadIds GetWithConfigureAwaitWithResult()
    {
        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before service call: {ThreadId}", before);

        var result = myService.SomeVeryImportantOperationWithConfigureAwaitAsync().Result;

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after service call: {ThreadId}", after);

        return new AllThreadIds(before, after, result.Before, result.After);
    }

    [HttpGet("f")]
    public AllThreadIds GetWithoutConfigureAwaitWithResult()
    {
        var before = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID before service call: {ThreadId}", before);

        var result = myService.SomeVeryImportantOperationAsync().Result;

        var after = Environment.CurrentManagedThreadId;
        logger.LogInformation("Thread ID after service call: {ThreadId}", after);

        return new AllThreadIds(before, after, result.Before, result.After);
    }
}