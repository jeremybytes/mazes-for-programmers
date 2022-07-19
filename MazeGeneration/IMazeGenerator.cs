using SixLabors.ImageSharp;

namespace MazeGeneration;

public interface IMazeGenerator
{
    void GenerateMaze();
    Image GetGraphicalMaze(bool includeHeatMap = false);
    string GetTextMaze(bool includePath = false);
}