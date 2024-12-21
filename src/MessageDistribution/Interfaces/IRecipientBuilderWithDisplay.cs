using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

public interface IRecipientBuilderWithDisplay : IRecipientBuilder
{
    IRecipientBuilderWithDisplay SetDisplay(IDisplay display);

    IRecipientBuilderWithDisplay SetColor(Color color);
}