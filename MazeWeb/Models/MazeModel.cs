using MazeGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MazeWeb.Models
{
    public class MazeModel
    {
        public int Size { get; set; }
        public string Algo { get; set; }
        public MazeColor Color { get; set; }
    }
}
