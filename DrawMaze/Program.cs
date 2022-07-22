using Algorithms;
using MazeGeneration;
using MazeGrid;
using SixLabors.ImageSharp;
using System.Diagnostics;

namespace DrawMaze;

class Program
{
    static void Main(string[] args)
    {
        IMazeGenerator generator =
            new MazeGenerator(
                new ColorGrid(15, 15),
                new HuntAndKill()
            );

        CreateAndShowMaze(generator);
        Console.ReadLine();
    }

    private static void CreateAndShowMaze(IMazeGenerator generator)
    {
        generator.GenerateMaze();

        var textMaze = generator.GetTextMaze(true);
        Console.WriteLine(textMaze);

        var graphicMaze = generator.GetGraphicalMaze(true);
        graphicMaze.SaveAsPng("maze.png");

        // This code is Windows-only
        // Comment out the following if building on macOS or Linux
        // The "maze.png" file can be located in the output
        // folder: [solutionlocation]/DrawMaze/bin/Debug/net6.0/
        Process p = new Process();
        p.StartInfo.UseShellExecute = true;
        p.StartInfo.FileName = "maze.png";
        p.Start();
    }
}
