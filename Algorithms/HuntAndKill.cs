using MazeGrid;

namespace Algorithms;

public class HuntAndKill : IMazeAlgorithm
{
    public void CreateMaze(Grid grid)
    {
        var rnd = new Random();
        Cell? current = grid.RandomCell();

        while (current != null)
        {
            List<Cell> unvisited_neighbors = current.Neighbors.Where(c => c.Links().Count == 0).ToList();

            if (unvisited_neighbors.Any())
            {
                int index = rnd.Next(unvisited_neighbors.Count);
                Cell neighbor = unvisited_neighbors[index];
                current.Link(neighbor);
                current = neighbor;
            }
            else
            {
                current = null;

                foreach(var cell in grid.EachCell())
                {
                    List<Cell> visited_neighbors = cell.Neighbors.Where(c => c.Links().Any()).ToList();
                    if (cell.Links().Count == 0 && visited_neighbors.Any())
                    {
                        current = cell;

                        int index = rnd.Next(visited_neighbors.Count());
                        var neighbor = visited_neighbors[index];
                        current.Link(neighbor);
                        break;
                    }
                }
            }
        }
    }
}
