# mazes-for-programmers
Coding along with "Mazes for Programmers" by ~~Jamis Buck~~

Updates:  
**August 2024**
* Upgraded code to .NET 8.0.

**July 2022**
* Upgraded code to .NET 6.0.
* Changed from using System.Drawing to ImageSharp package. This takes care of the cross-platform issues noted below (GDI+ is no longer required (which is good because GDI+ is not supported in .NET 6 for non-Windows platforms).)
* Adds scaling for output images. Smaller grids have larger cells; larger grids have smaller cells. This makes the image generation faster for larger grids.

**Jan 2020**: 
* Moved code to .NET Core and added a web interface. 
* Running the "MazeWeb" project provides a parameterized way to generate mazes. Graphical mazes are shown in the browser.

~~**Cross-Platform Note**:~~  
~~This project uses GDI+. If you're running on macOS or Linux and you get a GDI+ error, just follow the advice in this article: [SOLVED: System.Drawing .NETCore on Mac, GDIPlus Exception](https://medium.com/@hudsonmendes/solved-system-drawing-netcore-on-mac-gdiplus-exception-c455ab3655a2)~~  

**Articles**  
Building the Mazes: 
* [Approaching Functional Programming Completely Wrong](https://jeremybytes.blogspot.com/2017/03/approaching-function-programming.html)
* [More Maze Programming: Heat Map](https://jeremybytes.blogspot.com/2017/07/more-maze-programming-heat-map.html)
* [More Maze Programming: A Non-Biased Algorithm](https://jeremybytes.blogspot.com/2017/07/more-maze-programming-non-biased.html)
* [More Maze Programming: Adding Some Bias for Longer Paths](https://jeremybytes.blogspot.com/2017/07/more-maze-programming-adding-some-bias.html)  

.NET Core Conversion:
* [Converting .NET Framework to .NET Core - UseShellExecute has a Different Default Value](https://jeremybytes.blogspot.com/2019/08/converting-net-framework-to-net-core.html)  

Web Application:
* [Generating Mazes in a Browser](https://jeremybytes.blogspot.com/2020/01/generating-mazes-in-browser.html)  
* [Building a Web Front-End to Show Mazes in ASP.NET Core MVC](https://jeremybytes.blogspot.com/2020/01/building-web-front-end-to-show-mazes-in.html)
* Video Walkthrough: [CONDG Jan 2021 - ASP.NET MVC for Absolute Beginners](https://www.youtube.com/watch?v=dU1yyLPu2BE)  
*Note: If you've seen the "conventions" part of the presentation, you can start at 52:00 to see the MazeWeb part.*