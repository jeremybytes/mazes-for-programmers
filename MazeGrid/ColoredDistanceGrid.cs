using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGrid
{
    public class ColoredDistanceGrid : ColoredGrid
    {
        public ColoredDistanceGrid(int rows, int columns)
            : base(rows, columns) { }

        public override string ContentsOf(Cell cell)
        {
            if (distances != null &&
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
