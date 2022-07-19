using SixLabors.ImageSharp;
using System.Runtime.CompilerServices;

namespace MazeGeneration;

public class ConsoleLoggingDecorator : IMazeGenerator
{
    private readonly IMazeGenerator wrappedGenerator;

    public ConsoleLoggingDecorator(IMazeGenerator mazeGenerator)
    {
        wrappedGenerator = mazeGenerator;
    }

    private void LogToConsole(string message)
    {
        Console.WriteLine($"{DateTime.Now:s}: {message}");
    }

    private void LogEnterMethod([CallerMemberName] string? methodName = null)
    {
        LogToConsole($"Entering '{methodName}'");
    }

    private void LogExitMethod([CallerMemberName] string? methodName = null)
    {
        LogToConsole($"Exiting '{methodName}'");
    }

    public void GenerateMaze()
    {
        LogEnterMethod();
        wrappedGenerator.GenerateMaze();
        LogExitMethod();
    }

    public Image GetGraphicalMaze(bool includeHeatMap = false)
    {
        LogEnterMethod();
        var result = wrappedGenerator.GetGraphicalMaze(includeHeatMap);
        LogExitMethod();
        return result;
    }

    public string GetTextMaze(bool includePath = false)
    {
        LogEnterMethod();
        var result = wrappedGenerator.GetTextMaze(includePath);
        LogExitMethod();
        return result;
    }
}
