using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGrid
{
    public class ColoredGrid : Grid
    {
        private int maximum;

        public Distances distances { get; set; }

        public ColoredGrid(int rows, int columns)
            : base(rows, columns)
        {
            includeBackgrounds = true;
        }

        public override Color BackgroundColorFor(Cell cell)
        {
            maximum = distances.Values.Max();
            if (distances != null &&
                distances.ContainsKey(cell))
            {
                int distance = distances[cell];
                float intensity = ((float)maximum - (float)distance) / (float)maximum;
                int dark = Convert.ToInt32(255 * intensity);
                int bright = 128 + Convert.ToInt32(127 * intensity);
                return Color.FromArgb(dark, bright, dark);
            }
            else
            {
                return Color.White;
            }
        }

    }
}
