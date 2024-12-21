using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

public interface IDisplayDriver
{
    void Clear();

    void SetColor(Color color);

    void Write(string text);
}