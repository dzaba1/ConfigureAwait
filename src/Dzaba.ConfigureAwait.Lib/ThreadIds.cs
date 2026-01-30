namespace Dzaba.ConfigureAwait.Lib;

public class ThreadIds(int before, int after)
{
    public int Before { get; } = before;
    public int After { get; } = after;
}