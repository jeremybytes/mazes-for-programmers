using Algorithms;
using MazeGeneration;
using MazeGrid;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace MazeWeb.Controllers
{
    public class MazeController : Controller
    {
        public IActionResult Index(int size, string algo, MazeColor color)
        {
            int mazeSize = 15;
            if (size > 0)
            {
                mazeSize = size;
            }

            IMazeAlgorithm algorithm = new RecursiveBacktracker();
            if (!string.IsNullOrEmpty(algo))
            {
                Assembly assembly = Assembly.GetAssembly(typeof(RecursiveBacktracker));
                Type algoType = assembly.GetType($"Algorithms.{algo}", false, true);
                if (algoType != null)
                {
                    algorithm = Activator.CreateInstance(algoType) as IMazeAlgorithm;
                }
            }

            Bitmap mazeImage = Generate(mazeSize, algorithm, color);
            byte[] byteArray = ConvertToByteArray(mazeImage);
            return File(byteArray, "image/png");
        }

        public static byte[] ConvertToByteArray(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public Bitmap Generate(int mazeSize, IMazeAlgorithm algorithm, MazeColor color)
        {
            IMazeGenerator generator =
                new MazeGenerator(
                    new ColorGrid(mazeSize, mazeSize, color),
                    algorithm);

            generator.GenerateMaze();

            return generator.GetGraphicalMaze(true);
        }

    }
}