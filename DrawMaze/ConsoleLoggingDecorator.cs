using MazeGeneration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DrawMaze
{
    public class ConsoleLoggingDecorator : IMazeGenerator
    {
        IMazeGenerator wrappedGenerator;

        public ConsoleLoggingDecorator(IMazeGenerator mazeGenerator)
        {
            wrappedGenerator = mazeGenerator;
        }

        public void GenerateMaze()
        {
            LogEnterMethod();
            wrappedGenerator.GenerateMaze();
            LogExitMethod();
        }

        public Bitmap GetGraphicalMaze(bool includeHeatMap = false)
        {
            LogEnterMethod();
            var result = wrappedGenerator.GetGraphicalMaze(includeHeatMap);
            LogExitMethod();
            return result;
        }

        public string GetTextMaze(bool includePath = false)
        {
            LogEnterMethod();
            var result = wrappedGenerator.GetTextMaze(includePath);
            LogExitMethod();
            return result;
        }

        private void LogToConsole(string message)
        {
            Console.WriteLine($"{DateTime.Now:s}: {message}");
        }

        private void LogEnterMethod([CallerMemberName] string methodName = null)
        {
            LogToConsole($"Entering '{methodName}'");
        }

        private void LogExitMethod([CallerMemberName] string methodName = null)
        {
            LogToConsole($"Exiting '{methodName}'");
        }
    }
}
