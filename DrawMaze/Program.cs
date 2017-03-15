using Algorithms;
using MazeGrid;
using System;
using System.Collections.Generic;
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
            var grid = new Grid(15, 15);
            var maze = Sidewinder.CreateMaze(grid);
            Console.WriteLine(maze);

            Bitmap img = maze.ToPng(30);
            img.Save("maze.png");


            Console.ReadLine();
        }
    }
}
