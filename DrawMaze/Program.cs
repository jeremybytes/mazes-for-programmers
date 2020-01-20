using Algorithms;
using MazeGeneration;
using MazeGrid;
using System;
using System.Diagnostics;

namespace DrawMaze
{
    class Program
    {
        static void Main(string[] args)
        {
            IMazeGenerator generator =
                new MazeGenerator(
                    new ColorGrid(150, 150),
                    new RecursiveBacktracker());

            CreateAndShowMaze(generator);
            Console.ReadLine();
        }

        private static void CreateAndShowMaze(IMazeGenerator generator)
        {
            generator.GenerateMaze();

            var textMaze = generator.GetTextMaze(true);
            Console.WriteLine(textMaze);

            var graphicMaze = generator.GetGraphicalMaze(true);
            graphicMaze.Save("maze.png");
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = "maze.png";
            p.Start();
        }
    }
}
