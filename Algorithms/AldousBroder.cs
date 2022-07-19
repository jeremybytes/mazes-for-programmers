using MazeGrid;

namespace Algorithms;

public class AldousBroder : IMazeAlgorithm
{
    public void CreateMaze(Grid grid)
    {
        var rnd = new Random();

        Cell cell = grid.RandomCell();
        int unvisited = grid.Size - 1;

        while (unvisited > 0)
        {
            int index = rnd.Next(cell.Neighbors.Count);
            Cell neighbor = cell.Neighbors[index];

            if (neighbor.Links().Count == 0)
            {
                cell.Link(neighbor);
                unvisited -= 1;
            }
            cell = neighbor;
        }
    }
}
