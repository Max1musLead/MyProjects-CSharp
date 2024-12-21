using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Directors;

public class RecipientDirector
{
    public IRecipient ConstructUserRecipient(IRecipientBuilderWithUser builder, IUser user, ILogger? logger = null, IMessageFilter? filter = null)
    {
        return builder.SetUser(user)
            .SetFilter(filter)
            .SetLogger(logger)
            .Build();
    }

    public IRecipient ConstructMessengerRecipient(IRecipientBuilder builder, ILogger? logger = null, IMessageFilter? filter = null)
    {
        return builder.SetFilter(filter)
            .SetLogger(logger)
            .Build();
    }

    public IRecipient ConstructDisplayRecipient(IRecipientBuilderWithDisplay builder, IDisplay display, Color color, ILogger? logger = null, IMessageFilter? filter = null)
    {
        return builder.SetDisplay(display)
            .SetColor(color)
            .SetFilter(filter)
            .SetLogger(logger)
            .Build();
    }

    public IRecipient ConstructGroupRecipient(IRecipientBuilder builder, ILogger? logger = null, IMessageFilter? filter = null)
    {
        return builder.SetFilter(filter)
            .SetLogger(logger)
            .Build();
    }
}