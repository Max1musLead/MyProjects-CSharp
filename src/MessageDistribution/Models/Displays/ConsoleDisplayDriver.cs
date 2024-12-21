using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using System.Drawing;
using static Crayon.Output;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Displays;

public class ConsoleDisplayDriver : IDisplayDriver
{
    private Color _currentColor;

    public void Clear()
    {
        Console.Clear();
    }

    public void SetColor(Color color)
    {
        _currentColor = color;
    }

    public void Write(string text)
    {
        string coloredText = Rgb(_currentColor.R, _currentColor.G, _currentColor.B).Text(text);
        Console.WriteLine(coloredText);
    }
}