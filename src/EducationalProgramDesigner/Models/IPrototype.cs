namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public interface IPrototype<out T>
{
    T Clone(User newAuthor);
}