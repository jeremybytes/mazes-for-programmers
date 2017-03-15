using Algorithms;
using MazeGrid;
using System;
using System.Collections.Generic;
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
            Console.ReadLine();
        }
    }
}
