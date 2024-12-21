using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using System.Drawing;
using static Crayon.Output;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Displays;

public class FileDisplayDriver : IDisplayDriver
{
    private readonly string _filePath;
    private Color _currentColor;

    public FileDisplayDriver(string filePath)
    {
        _filePath = filePath;
    }

    public void Clear()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
        }
    }

    public void SetColor(Color color)
    {
        _currentColor = color;
    }

    public void Write(string text)
    {
        string coloredText = Rgb(_currentColor.R, _currentColor.G, _currentColor.B).Text(text);
        File.WriteAllText(_filePath, coloredText);
    }
}