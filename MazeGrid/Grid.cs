using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;

namespace MazeGrid;

public abstract class Grid
{
    public int Rows { get; private set; }
    public int Columns { get; private set; }
    public int Size { get; private set; }
    public Cell[][] Cells { get; private set; }

    public abstract Distances? distances { get; set; }
    public abstract Distances? path { get; set; }

    protected bool includeBackgrounds = false;

    private Random rnd = new Random();

    public Grid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        Size = Rows * Columns;

        Cells = PrepareGrid();
        ConfigureCells();
    }

    public Cell[][] PrepareGrid()
    {
        var cells = new Cell[Rows][];
        for (int i = 0; i < Rows; i++)
        {
            cells[i] = new Cell[Columns];
            for (int j = 0; j < Columns; j++)
            {
                cells[i][j] = new Cell(i, j);
            }
        }
        return cells;
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

        foreach (Cell[] cellRow in EachRow())
        {
            string top = "|";
            string bottom = "+";

            foreach (Cell cell in cellRow)
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

    public Image ToPng(int cellSize = 10)
    {
        int imgWidth = cellSize * Columns;
        int imgHeight = cellSize * Rows;

        IPen wall = Pens.Solid(Color.Black, 1);

        Image<Rgba32> mazeImage = new(imgWidth, imgHeight);

        Rectangle rect = new(0, 0, imgHeight, imgWidth);
        mazeImage.Mutate(x => x.Fill(Color.White, rect));

        if (includeBackgrounds)
            foreach (var cell in EachCell())
            {
                int x1 = cell.Column * cellSize;
                int y1 = cell.Row * cellSize;
                int x2 = (cell.Column + 1) * cellSize;
                int y2 = (cell.Row + 1) * cellSize;

                Color c = BackgroundColorFor(cell);
                Rectangle r = new(x1, y1, (x2 - x1), (y2 - y1));
                mazeImage.Mutate(x => x.Fill(c, r));
            }

        foreach (var cell in EachCell())
        {
            int x1 = cell.Column * cellSize;
            int y1 = cell.Row * cellSize;
            int x2 = (cell.Column + 1) * cellSize;
            int y2 = (cell.Row + 1) * cellSize;



            if (cell.North == null)
                mazeImage.Mutate(x => x.DrawLines(wall, new Point(x1, y1), new Point(x2, y1)));
            if (cell.West == null)
                mazeImage.Mutate(x => x.DrawLines(wall, new Point(x1, y1), new Point(x1, y2)));
            if (!cell.IsLinked(cell.East))
                mazeImage.Mutate(x => x.DrawLines(wall, new Point(x2, y1), new Point(x2, y2)));
            if (!cell.IsLinked(cell.South))
                mazeImage.Mutate(x => x.DrawLines(wall, new Point(x1, y2), new Point(x2, y2)));
        }

        return mazeImage;
    }

}
