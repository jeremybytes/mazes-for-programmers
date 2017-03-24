using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGrid
{
    public class Distances : Dictionary<Cell, int>
    {
        Cell root;

        public Distances(Cell root)
            : base()
        {
            this.root = root;
            this[root] = 0;
        }

        public KeyCollection cells()
        {
            return this.Keys;
        }

        public void SetDistance(Cell cell, int distance)
        {
            if (!this.ContainsKey(cell))
                this.Add(cell, distance);
            else
                this[cell] = distance;
        }
    }
}
