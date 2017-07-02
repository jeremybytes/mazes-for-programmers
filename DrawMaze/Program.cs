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
            var grid = new ColoredDistanceGrid(15, 15);
            var maze = Sidewinder.CreateMaze(grid);

            var start = maze.GetCell(grid.Rows/2, grid.Columns/2);
            maze.distances = start.distances().PathTo(grid.GetCell(grid.Rows - 1, 0));

            Console.WriteLine(maze);

            maze.distances = start.distances();

            Bitmap img = maze.ToPng(30);
            img.Save("maze.png");

            Process p = new Process();
            p.StartInfo.FileName = "maze.png";
            p.Start();

            Console.ReadLine();
        }
    }
}
