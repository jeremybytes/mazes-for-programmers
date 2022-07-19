namespace MazeGrid;

public class Cell
{
    public int Row { get; private set; }
    public int Column { get; private set; }

    public Cell? North { get; set; }
    public Cell? South { get; set; }
    public Cell? East { get; set; }
    public Cell? West { get; set; }

    public List<Cell> Neighbors
    {
        get
        {
            var list = new List<Cell>();
            if (North != null) list.Add(North);
            if (South != null) list.Add(South);
            if (East != null) list.Add(East);
            if (West != null) list.Add(West);
            return list;
        }
    }

    private Dictionary<Cell, bool> links;

    public Cell(int row, int column)
    {
        Row = row;
        Column = column;
        links = new Dictionary<Cell, bool>();
    }

    public Cell Link(Cell cell, bool bidi = true)
    {
        links[cell] = true;
        if (bidi)
            cell.Link(this, false);
        return this;
    }

    public Cell Unlink(Cell cell, bool bidi = true)
    {
        links.Remove(cell);
        if (bidi)
            cell.Unlink(this, false);
        return this;
    }

    public List<Cell> Links()
    {
        return links.Keys.ToList();
    }

    public bool IsLinked(Cell? cell)
    {
        if (cell == null)
            return false;
        return links.ContainsKey(cell);
    }

    public Distances GetDistances()
    {
        var distances = new Distances(this);
        List<Cell> frontier = new List<Cell>() { this };

        while (frontier.Any())
        {
            List<Cell> new_frontier = new List<Cell>();

            foreach(var cell in frontier)
            {
                foreach(var linked in cell.Links())
                {
                    if (distances.ContainsKey(linked)) continue;
                    distances.Add(linked, distances[cell] + 1);
                    new_frontier.Add(linked);
                }

            }
            frontier = new_frontier;
        }
        return distances;
    }
}
