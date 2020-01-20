using System;
using System.Drawing;
using System.Linq;

namespace MazeGrid
{
    public enum MazeColor
    {
        White = 0,
        Teal,
        Purple,
        Mustard,
        Rust,
        Green,
        Blue,
    }

    public class ColorGrid : Grid
    {
        private int? maximum;

        public override Distances distances { get; set; }
        public override Distances path { get; set; }
        public MazeColor color { get; set; }

        public ColorGrid(int rows, int columns, MazeColor color = MazeColor.Purple)
            : base(rows, columns)
        {
            includeBackgrounds = true;
            this.color = color;
        }

        public override string ContentsOf(Cell cell)
        {
            if (path != null &&
                path.ContainsKey(cell))
            {
                return path[cell].ToString().Last().ToString();
            }
            {
                return " ";
            }
        }

        public override Color BackgroundColorFor(Cell cell)
        {
            maximum = distances?.Values.Max();
            if (distances != null &&
                distances.ContainsKey(cell))
            {
                int distance = distances[cell];
                float intensity = ((float)maximum - (float)distance) / (float)maximum;
                int dark = Convert.ToInt32(255 * intensity);
                int bright = 128 + Convert.ToInt32(127 * intensity);

                switch (color)
                {
                    case MazeColor.White:
                        return Color.White;
                    case MazeColor.Teal:
                        return Color.FromArgb(dark, bright, bright);
                    case MazeColor.Purple:
                        return Color.FromArgb(bright, dark, bright);
                    case MazeColor.Mustard:
                        return Color.FromArgb(bright, bright, dark);
                    case MazeColor.Rust:
                        return Color.FromArgb(bright, dark, dark);
                    case MazeColor.Green:
                        return Color.FromArgb(dark, bright, dark);
                    case MazeColor.Blue:
                        return Color.FromArgb(dark, dark, bright);
                    default:
                        return Color.FromArgb(dark, bright, bright);
                }
            }
            else
            {
                return Color.White;
            }
        }

    }
}
