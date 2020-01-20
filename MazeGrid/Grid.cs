using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeGrid
{
    public abstract class Grid
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int Size { get; private set; }
        public Cell[][] Cells { get; private set; }

        public abstract Distances distances { get; set; }
        public abstract Distances path { get; set; }

        protected bool includeBackgrounds = false;

        private Random rnd = new Random();

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Size = Rows * Columns;

            PrepareGrid();
            ConfigureCells();
        }

        public void PrepareGrid()
        {
            Cells = new Cell[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                Cells[i] = new Cell[Columns];
                for (int j = 0; j < Columns; j++)
                {
                    Cells[i][j] = new Cell(i, j);
                }
            }
        }

        public void ConfigureCells()
        {
            foreach (var cellRow in Cells)
                foreach (var cell in cellRow)
                {
                    int row = cell.Row;
                    int col = cell.Column;

                    if (row - 1 >= 0)
                        cell.North = Cells[row - 1][col];
                    if (row + 1 < Rows)
                        cell.South = Cells[row + 1][col];
                    if (col - 1 >= 0)
                        cell.West = Cells[row][col - 1];
                    if (col + 1 < Columns)
                        cell.East = Cells[row][col + 1];
                }
        }

        public Cell GetCell(int row, int column)
        {
            return Cells[row][column];
        }

        public Cell RandomCell()
        {
            int row = rnd.Next(Rows);
            int column = rnd.Next(Columns);
            return GetCell(row, column);
        }

        public IEnumerable<Cell[]> EachRow()
        {
            for (int i = 0; i < Rows; i++)
            {
                yield return Cells[i];
            }
        }

        public IEnumerable<Cell> EachCell()
        {
            foreach (var cellRow in Cells)
                foreach (var cell in cellRow)
                    yield return cell;
        }

        public virtual string ContentsOf(Cell cell)
        {
            return " ";
        }

        public virtual Color BackgroundColorFor(Cell cell)
        {
            return Color.White;
        }

        public override string ToString()
        {
            if (this.Columns > 20 || this.Rows > 20)
            {
                return "Grid is too large for text output";
            }

            string Output = "+";
            for (int i = 0; i < this.Columns; i++)
            {
                Output += "---+";
            }
            Output += "\n";

            foreach (var cellRow in EachRow())
            {
                string top = "|";
                string bottom = "+";

                foreach (var cell in cellRow)
                {
                    string body = $" {ContentsOf(cell)} ";
                    string eastBoundary = (cell.IsLinked(cell.East) ? " " : "|");

                    top += body + eastBoundary;

                    string southBoundary = (cell.IsLinked(cell.South) ? "   " : "---");
                    string corner = "+";

                    bottom += southBoundary + corner;
                }

                Output += top + "\n";
                Output += bottom + "\n";
            }

            return Output;
        }

        public Bitmap ToPng(int cellSize = 10)
        {
            int imgWidth = cellSize * Columns;
            int imgHeight = cellSize * Rows;

            Brush background = Brushes.White;
            Pen wall = Pens.Black;

            Bitmap mazeImage = new Bitmap(imgWidth, imgHeight);

            using (var graphics = Graphics.FromImage(mazeImage))
            {
                graphics.FillRectangle(background, 0, 0, imgWidth, imgHeight);

                if (includeBackgrounds)
                    foreach (var cell in EachCell())
                    {
                        int x1 = cell.Column * cellSize;
                        int y1 = cell.Row * cellSize;
                        int x2 = (cell.Column + 1) * cellSize;
                        int y2 = (cell.Row + 1) * cellSize;

                        Color color = BackgroundColorFor(cell);
                        Brush brush = new SolidBrush(color);
                        graphics.FillRectangle(brush, x1, y1, (x2 - x1), (y2 - y1));
                    }

                foreach (var cell in EachCell())
                {
                    int x1 = cell.Column * cellSize;
                    int y1 = cell.Row * cellSize;
                    int x2 = (cell.Column + 1) * cellSize;
                    int y2 = (cell.Row + 1) * cellSize;

                    if (cell.North == null)
                        graphics.DrawLine(wall, x1, y1, x2, y1);
                    if (cell.West == null)
                        graphics.DrawLine(wall, x1, y1, x1, y2);
                    if (!cell.IsLinked(cell.East))
                        graphics.DrawLine(wall, x2, y1, x2, y2);
                    if (!cell.IsLinked(cell.South))
                        graphics.DrawLine(wall, x1, y2, x2, y2);
                }
            }

            return mazeImage;
        }

    }
}
