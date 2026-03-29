namespace SharpLisp.Listener;

public class Listener
{
    private readonly ListenerCommandResolver _listenerCommandResolver = new ListenerCommandResolver();
    
    public void Run()
    {
        _listenerCommandResolver.Init();
        Console.WriteLine("-------------------");
        Console.WriteLine("SharpLisp listener");
        Console.WriteLine("-------------------\n");
        Console.WriteLine("Type '#HELP' for help.\n");

        while (true)
        {
            // lepší než si to psát sám, má to i historii
            var input = ReadLine.Read("> ");
            ReadLine.AddHistory(input);
            _listenerCommandResolver.ResolveCommand(input?.Trim() ?? "");
            Console.WriteLine();
        }
    }
}
