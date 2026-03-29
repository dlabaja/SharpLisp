using System.Diagnostics;
using System.Text;

namespace SharpLisp.Listener;

public class ListenerControls
{
    public void Run()
    {
        var thread = new Thread(ControlsLoop);
        thread.Start();
    }

    private void SetCursor(int dx, int minX, ref int currentPos)
    {
        Console.SetCursorPosition(Math.Max(minX, Console.CursorLeft + dx), Console.CursorTop);
        currentPos = Math.Max(minX, currentPos + dx);
    }

    private void ProcessEnter(string command)
    {
        Console.WriteLine();
        ListenerCommands.ResolveCommand(command);
        Console.Write("> ");
    }

    private void ProcessBackspace(StringBuilder buffer, int startPos, ref int currentPos)
    {
        SetCursor(-1, startPos, ref currentPos);
        try
        {
            Debug.WriteLine("index " + Math.Max(0, currentPos - startPos));
            buffer.Remove(Math.Max(0, currentPos - startPos), 1);
        }
        catch{}

        RedrawLine(buffer.ToString(), startPos, currentPos);
    }

    private void RedrawLine(string newContent, int startPos, int currentPos)
    {
        Console.SetCursorPosition(startPos, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(startPos, Console.CursorTop - 1);
        Console.Write(newContent);
        Console.SetCursorPosition(currentPos, Console.CursorTop);
    }

    private void ControlsLoop()
    {
        var buffer = new StringBuilder();
        var startPos = Console.GetCursorPosition().Left;
        var currentPos = startPos;
        while (true)
        {
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    SetCursor(-1, startPos, ref currentPos);
                    break;
                case ConsoleKey.RightArrow:
                    SetCursor(1, startPos, ref currentPos);
                    break;
                case ConsoleKey.Backspace:
                    ProcessBackspace(buffer, startPos, ref currentPos);
                    break;
                case ConsoleKey.Enter:
                    ProcessEnter(buffer.ToString());
                    buffer = new StringBuilder();
                    break;
                default:
                    buffer.Insert(currentPos - startPos, key.KeyChar);
                    currentPos++;
                    RedrawLine(buffer.ToString(), startPos, currentPos);
                    break;
            }
            Debug.WriteLine(buffer.ToString());
            Debug.WriteLine(currentPos);
        }
    }
}
