using System.Drawing;

namespace MazeGeneration
{
    public interface IMazeGenerator
    {
        void GenerateMaze();
        Bitmap GetGraphicalMaze(bool includeHeatMap = false);
        string GetTextMaze(bool includePath = false);
    }
}