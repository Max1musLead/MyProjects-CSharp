using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

public interface IDisplay
{
    void DisplayOnscreen(string message, Color color);
}