using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGrid
{
    public class DistanceGrid : Grid
    {
        public Distances distances { get; set; }

        public DistanceGrid(int rows, int columns)
            : base(rows, columns) { }

        public override string ContentsOf(Cell cell)
        {
            if (distances!= null &&
                distances.ContainsKey(cell))
            {
                return distances[cell].ToString().Last().ToString();
            }
            {
                return " ";
            }
        }
    }
}
