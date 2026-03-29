namespace SharpLisp.Listener;

public static class Listener
{
    public static void Run()
    {
        new ListenerControls().Run();
        Console.WriteLine("-------------------");
        Console.WriteLine("SharpLisp listerner");
        Console.WriteLine("-------------------\n");
        Console.WriteLine("Type '#HELP' for help.\n");

        Console.Write("> ");
    }
}
