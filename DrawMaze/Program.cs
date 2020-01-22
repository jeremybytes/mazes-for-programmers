using Algorithms;
using MazeGeneration;
using MazeGrid;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace DrawMaze
{
    class Program
    {
        static void Main(string[] args)
        {
            IMazeGenerator generator =
                new MazeGenerator(
                    new ColorGrid(20, 20),
                    new RecursiveBacktracker());

            CreateAndShowMaze(generator);
            Console.ReadLine();
        }

        private static void CreateAndShowMaze(IMazeGenerator generator)
        {
            generator.GenerateMaze();

            string textMaze = generator.GetTextMaze(true);
            Console.WriteLine(textMaze);

            Bitmap graphicMaze = generator.GetGraphicalMaze(true);
            graphicMaze.Save("maze.png", ImageFormat.Png);
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = "maze.png";
            p.Start();
        }
    }
}
