using MazeGrid;

namespace MazeWeb.Models;

public class MazeModel
{
    public int Size { get; set; }
    public string? Algo { get; set; }
    public MazeColor Color { get; set; }
}
