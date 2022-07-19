namespace MazeGrid;

public class Distances : Dictionary<Cell, int>
{
    Cell root;

    public Distances(Cell root)
        : base()
    {
        this.root = root;
        this[root] = 0;
    }

    public List<Cell> cells()
    {
        return this.Keys.ToList();
    }

    public void SetDistance(Cell cell, int distance)
    {
        if (!this.ContainsKey(cell))
            this.Add(cell, distance);
        else
            this[cell] = distance;
    }

    public Distances PathTo(Cell goal)
    {
        Cell current = goal;

        var breadcrumbs = new Distances(root);
        breadcrumbs[current] = this[current];

        while (current != root)
        {
            foreach(var neighbor in current.Links())
            {
                if(this[neighbor] < this[current])
                {
                    if (!breadcrumbs.ContainsKey(neighbor))
                        breadcrumbs.Add(neighbor, this[neighbor]);
                    else
                        breadcrumbs[neighbor] = this[neighbor];
                    current = neighbor;
                    break;
                }
            }
        }
        return breadcrumbs;
    }
}
