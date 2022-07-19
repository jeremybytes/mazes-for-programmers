using Algorithms;
using MazeGeneration;
using MazeGrid;
using SixLabors.ImageSharp;
using System.Diagnostics;
using Ninject;

namespace DrawMaze;

class Program
{
    static void Main(string[] args)
    {
        // Manual composition
        //IMazeGenerator generator =
        //    new ConsoleLoggingDecorator(
        //        new MazeGenerator(
        //            new ColorGrid(15, 15),
        //            new HuntAndKill())
        //    );

        // Ninject DI container
        IKernel container = new StandardKernel();
        container.Bind<Grid>().ToMethod(c => new ColorGrid(50, 50));
        container.Bind<IMazeAlgorithm>().To<HuntAndKill>();
        container.Bind<IMazeGenerator>().To<ConsoleLoggingDecorator>()
            .WithConstructorArgument<IMazeGenerator>(container.Get<MazeGenerator>());

        IMazeGenerator generator = container.Get<IMazeGenerator>();

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
