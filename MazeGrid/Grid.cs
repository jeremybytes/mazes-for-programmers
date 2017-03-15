using System;
using System.Collections.Generic;

namespace MazeGrid
{
    public class Grid
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public Cell[][] Cells { get; private set; }

        private Random rnd = new Random();

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

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

        public override string ToString()
        {
            string Output = "+";
            for (int i = 0; i < this.Columns; i++)
            {
                Output += "---+";
            }
            Output += "\n";

            foreach(var cellRow in EachRow())
            {
                string top = "|";
                string bottom = "+";

                foreach(var cell in cellRow)
                {
                    string body = "   ";
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

    }
}
