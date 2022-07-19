using MazeGrid;

namespace Algorithms;

public class Sidewinder : IMazeAlgorithm
{
    public void CreateMaze(Grid grid)
    {
        var rnd = new Random();

        foreach(var cellRow in grid.EachRow())
        {
            var run = new List<Cell>();

            foreach(var cell in cellRow)
            {
                run.Add(cell);

                bool atEasternBoundary = (cell.East == null);
                bool atNorthernBoundary = (cell.North == null);

                bool shouldCloseOut =
                    atEasternBoundary ||
                    (!atNorthernBoundary && rnd.Next(2) == 0);
                   
                if (shouldCloseOut)
                {
                    Cell member = run[rnd.Next(run.Count - 1)];
                    if (member.North != null)
                    {
                        member.Link(member.North);
                    }
                    run.Clear();
                }
                else
                {
                    cell.Link(cell.East!);
                }
            }
        }
    }
}
