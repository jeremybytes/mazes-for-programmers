using Algorithms;
using MazeGrid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMaze
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new DistanceGrid(15, 15);
            var maze = BinaryTree.CreateMaze(grid);

            var start = maze.GetCell(0, 0);
            var distances = start.distances();
            maze.distances = distances.PathTo(grid.GetCell(grid.Rows-1, 0));

            Console.WriteLine(maze);

            Bitmap img = maze.ToPng(30);
            img.Save("maze.png");

            Process p = new Process();
            p.StartInfo.FileName = "maze.png";
            p.Start();

            Console.ReadLine();
        }
    }
}
