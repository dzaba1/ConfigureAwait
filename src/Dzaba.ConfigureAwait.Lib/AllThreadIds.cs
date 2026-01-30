namespace Dzaba.ConfigureAwait.Lib;

public class AllThreadIds(int before, int after, int serviceBefore, int serviceAfter)
{
    public int Before { get; } = before;
    public int After { get; } = after;
    public int ServiceBefore { get; } = serviceBefore;
    public int ServiceAfter { get; } = serviceAfter;
}