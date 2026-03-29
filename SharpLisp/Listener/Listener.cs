namespace SharpLisp.Listener;

public static class Listener
{
    public static void Run()
    {
        Console.WriteLine("-------------------");
        Console.WriteLine("SharpLisp listerner");
        Console.WriteLine("-------------------\n");
        Console.WriteLine("Type '#HELP' for help.\n");

        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            ListenerCommands.ResolveCommand(input?.Trim() ?? "");
            Console.WriteLine();
        }
    }
}
