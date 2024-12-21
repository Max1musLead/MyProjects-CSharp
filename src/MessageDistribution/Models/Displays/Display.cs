using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Displays;

public class Display : IDisplay
{
    private readonly IDisplayDriver _displayDriver;

    public Display(IDisplayDriver displayDriver)
    {
        _displayDriver = displayDriver;
    }

    public void DisplayOnscreen(string message, Color color)
    {
        _displayDriver.Clear();
        _displayDriver.SetColor(color);
        _displayDriver.Write(message);
    }
}