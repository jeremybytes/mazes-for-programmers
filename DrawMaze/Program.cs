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
            // Choose grid
            //var grid = new Grid(15, 15); // plain grid, no annotations
            //var grid = new DistanceGrid(15, 15); // "path" on text grid
            //var grid = new ColoredGrid(15, 15); // heat-map on graphical grid
            var grid = new ColoredDistanceGrid(15, 15); // "path" and heat-map

            // Choose maze algorithm
            // var maze = Sidewinder.CreateMaze(grid); // vertical bias
            // var maze = BinaryTree.CreateMaze(grid); // diagonal bias
            // var maze = AldousBroder.CreateMaze(grid); // non-biases (slow)
            // var maze = HuntAndKill.CreateMaze(grid); // some bias (twistier paths)
            var maze = RecursiveBacktracker.CreateMaze(grid); // some bias (longer paths)

            // Locate "start" cell in the center (for distances)
            var start = maze.GetCell(grid.Rows/2, grid.Columns/2);

            // For text-based maze, use "Path" distance (one path)
            maze.distances = start.distances().PathTo(grid.GetCell(grid.Rows - 1, 0));

            // Draw text-based maze
            Console.WriteLine(maze);

            // For graphical maze, use all distances (heat map)
            maze.distances = start.distances();

            // Draw graphical maze
            Bitmap img = maze.ToPng(30);
            img.Save("maze.png");

            // Show saved maze image
            Process p = new Process();
            p.StartInfo.FileName = "maze.png";
            p.Start();

            Console.ReadLine();
        }
    }
}
