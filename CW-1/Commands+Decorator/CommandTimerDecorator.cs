namespace CW_1.Commands;

public class CommandTimerDecorator : ICommand
{
    private readonly ICommand _inner;

    public CommandTimerDecorator(ICommand inner)
    {
        _inner = inner;
    }

    public void Execute()
    {
        var start = DateTime.Now;
        _inner.Execute();
        var end = DateTime.Now;
        Console.WriteLine($"Команда выполнена за {(end - start).TotalMilliseconds} мс");
    }
}