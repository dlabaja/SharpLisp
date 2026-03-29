using SharpLisp.Defined;

namespace SharpLisp.Listener;

public class ListenerCommands
{
    public static void ResolveCommand(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
        {
            return;
        }

        var res = ResolveCommandByStart(command, "#END", EndCommand) ||
                  ResolveCommandByStart(command, "#ENV", EnvCommand) ||
                  ResolveCommandByStart(command, "#HELP", HelpCommand);
                  ResolveCommandByStart(command, "#LOAD", LoadCommand);

        if (!res)
        {
            Interpreter.Eval(command);
        }
    }

    private static bool ResolveCommandByStart(string command, string start, Action<string> function)
    {
        if (command.StartsWith(start, StringComparison.CurrentCultureIgnoreCase))
        {
            function(command);
            return true;
        }

        return false;
    }

    private static void EndCommand(string command)
    {
        Console.WriteLine("Exiting listener...");
        Environment.Exit(0);
    }
    
    private static void EnvCommand(string command)
    {
        var env = GlobalEnvironment.Environment;
        PrintEnv("Special operators:", 
            ["QUOTE", "IF", "LAMBDA", "FUNCTION", "FUNCALL", "DEFUN", "DEFMACRO", "LABELS", "ERROR"]);
        PrintEnv("Macros:", env.GetMacroNames());
        PrintEnv("Primitives:", env.GetPrimitiveNames());
        PrintEnv("Functions:", env.GetFunctionNames());
        PrintEnv("Consts:", env.GetValueNames());
    }

    private static void PrintEnv(string name, List<string> values)
    {
        if (values.Count == 0)
        {
            return;
        }
        Console.WriteLine(name);
        Console.WriteLine($"{string.Join(", ", values)}");
    }

    private static void LoadCommand(string command)
    {
        
    }

    private static void HelpCommand(string command)
    {
        Console.WriteLine("#END - closes the listener");
        Console.WriteLine("#ENV - prints all available operators, macros, functions, primitives and consts");
        Console.WriteLine("#HELP - prints this");
        Console.WriteLine("#LOAD \"<path>\" - loads expressions from file, needs to be in apostrophes");
    }
}
