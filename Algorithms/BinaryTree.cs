using MazeGrid;

namespace Algorithms;

public class BinaryTree : IMazeAlgorithm
{
    public void CreateMaze(Grid grid)
    {
        var rnd = new Random();

        foreach (var cell in grid.EachCell())
        {
            var neighbors = new List<Cell>();
            if (cell.North != null)
                neighbors.Add(cell.North);
            if (cell.East != null)
                neighbors.Add(cell.East);

            int index = rnd.Next(neighbors.Count);
            if (index < neighbors.Count)
            {
                Cell neighbor = neighbors[index];
                cell.Link(neighbor);
            }
        }
    }
}
