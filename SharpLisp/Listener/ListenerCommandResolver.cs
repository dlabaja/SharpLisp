using SharpLisp.DataTypes;
using SharpLisp.Defined;
using SharpLisp.Factories;
using SharpLisp.Utils;
using System.Text.RegularExpressions;
using Environment = System.Environment;

namespace SharpLisp.Listener;

public class ListenerCommandResolver
{
    public void Init()
    {
        InitHistoryValues();
    }

    public void ResolveCommand(string command)
    {
        try
        {
            Resolve(command);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }
    }

    private void Resolve(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
        {
            return;
        }

        var res = ResolveCommandByStart(command, "#END", EndCommand) ||
                  ResolveCommandByStart(command, "#ENV", EnvCommand) ||
                  ResolveCommandByStart(command, "#HELP", HelpCommand) ||
                  ResolveCommandByStart(command, "#LOAD", LoadCommand);

        if (!res)
        {
            var exp = Interpreter.Eval(command);
            RotateHistoryValues(exp);
            Console.WriteLine(exp);
        }
    }

    private void InitHistoryValues()
    {
        var env = GlobalEnvironment.Environment;
        env.AddValue("***", SymbolicExpressionFactory.Nil);
        env.AddValue("**", SymbolicExpressionFactory.Nil);
        env.AddValue("*", SymbolicExpressionFactory.Nil);
    }

    private void RotateHistoryValues(SymbolicExpression latestValue)
    {
        var env = GlobalEnvironment.Environment;
        env.TryGetValue("**", out var val2);
        env.TryGetValue("*", out var val1);
        env.AddValue("***", val2);
        env.AddValue("**", val1);
        env.AddValue("*", latestValue);
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
            ["QUOTE", "IF", "LAMBDA", "FUNCTION", "FUNCALL", "DEFUN", "DEFMACRO", "LABELS", "ERROR", "PROGN", "SETQ", "SET-CAR", "SET-CDR"]);
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
        Console.WriteLine($"    {string.Join(", ", values)}");
    }

    private static void LoadCommand(string command)
    {
        var path = Regex.Match(command, "\"(.*)\"").Groups[1].Value;
        LoadPath(path);
    }

    private static void LoadPath(string path)
    {
        var content = File.ReadAllText(path);
        var strExpr = "(list " + content + ")";
        Console.WriteLine(Interpreter.Eval(strExpr));
    }

    private static void HelpCommand(string command)
    {
        Console.WriteLine("#END - closes the listener");
        Console.WriteLine("#ENV - prints all available operators, macros, functions, primitives and consts");
        Console.WriteLine("#HELP - prints this");
        Console.WriteLine("#LOAD \"<path>\" - loads expressions from file, needs to be in apostrophes");
    }
}
