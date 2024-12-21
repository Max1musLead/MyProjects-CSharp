using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models;

public class ImportanceFilter : IMessageFilter
{
    private readonly int _minImportance;

    public ImportanceFilter(int minImportance)
    {
        _minImportance = minImportance;
    }

    public bool Filter(IMessage message)
    {
        return message.Importance >= _minImportance;
    }
}