# How `.ConfigureAwait(false)` works?

When `.ConfigureAwait(false)` is called and when `await` finishes then it don’t try to resume on the original context.
Just continue on any available thread.

First of all, check [MyService](src/Dzaba.ConfigureAwait.Lib/MyService.cs) class.
There are two `Task.Delay(3000)`. One with `.ConfigureAwait(false)` and second one without.

Then check [Dzaba.ConfigureAwait.Wpf](src/Dzaba.ConfigureAwait.Wpf) application.
There are 6 buttons calling [MyService](src/Dzaba.ConfigureAwait.Lib/MyService.cs) in different ways.
Click on them and observe what happens.

## Why and when use `.ConfigureAwait(false)`?

**Always** use it on library levels. You will never know how your libraries are really used. You can only suppose.

1. It solves the `.Result` deadlock. Imagine someone using your library in this way. Someone can blame you for making that deadlock.
2. Perfomance - in most cases you don't care on which context your code continue. Switching context is not for free.
3. UI usage - yes, your library will probably break someone else UI code but the problem can be easily reprododuced and fixed by `SynchronizationContext`, `BeginInvoke` or `Dispatcher` by the user.

Don't use `.ConfigureAwait(false)` when making UI code or you really want to be always on the original context.
