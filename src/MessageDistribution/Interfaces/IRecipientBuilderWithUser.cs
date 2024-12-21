namespace Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

public interface IRecipientBuilderWithUser : IRecipientBuilder
{
    IRecipientBuilder SetUser(IUser user);
}