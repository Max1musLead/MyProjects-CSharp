using Itmo.ObjectOrientedProgramming.Lab4.Outputs;
using Itmo.ObjectOrientedProgramming.Lab4.Visitor;
using Itmo.ObjectOrientedProgramming.Lab4.Visitor.FileSystemComponents;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

public class LocalFileSystem : IFileSystem
{
    public string? CurrentPath { get; private set; }

    public void Connect(string address)
    {
        if (!Directory.Exists(address))
        {
            throw new DirectoryNotFoundException("Директория не найдена.");
        }

        CurrentPath = Path.GetFullPath(address);
    }

    public void Disconnect()
    {
        CurrentPath = null;
    }

    public void ChangeDirectory(string path)
    {
        CurrentPath = Path.IsPathRooted(path)
            ? path
            : Path.GetFullPath(Path.Combine(CurrentPath ?? throw new ArgumentNullException(CurrentPath), path));
    }

    public void GetDirectoryTree(int depth)
    {
        ArgumentNullException.ThrowIfNull(CurrentPath);

        var factory = new FileSystemComponentFactory();
        IFileSystemComponent rootComponent = factory.Create(CurrentPath, depth);

        var visitor = new ConsoleVisitor(new ConsoleOutput());
        rootComponent.Accept(visitor);
    }

    public void ShowFile(string path, IOutput output)
    {
        string filePath = Path.IsPathRooted(path)
            ? path
            : Path.GetFullPath(Path.Combine(CurrentPath ?? throw new ArgumentNullException(CurrentPath), path));

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не найден.");
        }

        output.Display(File.ReadAllText(filePath));
    }

    public void MoveFile(string sourcePath, string destinationPath)
    {
        string sourceFile = Path.IsPathRooted(sourcePath)
            ? sourcePath
            : Path.GetFullPath(Path.Combine(CurrentPath ?? throw new ArgumentNullException(CurrentPath), sourcePath));
        string moveDestinationPath = Path.IsPathRooted(destinationPath)
            ? destinationPath
            : Path.GetFullPath(Path.Combine(CurrentPath ?? throw new ArgumentNullException(CurrentPath), destinationPath));

        string destinationFile = Path.Combine(moveDestinationPath, Path.GetFileName(sourceFile));

        if (File.Exists(destinationFile))
        {
            throw new IOException("Файл уже существует.");
        }

        File.Move(sourceFile, destinationFile);
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
        string sourceFile = Path.IsPathRooted(sourcePath)
            ? sourcePath
            : Path.GetFullPath(Path.Combine(CurrentPath ?? throw new ArgumentNullException(CurrentPath), sourcePath));
        string moveDestinationPath = Path.IsPathRooted(destinationPath)
            ? destinationPath
            : Path.GetFullPath(Path.Combine(CurrentPath ?? throw new ArgumentNullException(CurrentPath), destinationPath));

        string destinationFile = Path.Combine(moveDestinationPath, Path.GetFileName(sourceFile));

        if (File.Exists(destinationFile))
        {
            throw new IOException("Файл уже существует.");
        }

        File.Copy(sourceFile, destinationFile);
    }

    public void DeleteFile(string path)
    {
        string filePath = Path.IsPathRooted(path)
            ? path
            : Path.GetFullPath(Path.Combine(CurrentPath ?? throw new ArgumentNullException(CurrentPath), path));

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не найден.");
        }

        File.Delete(filePath);
    }

    public void RenameFile(string path, string newName)
    {
        string filePath = Path.IsPathRooted(path)
            ? path
            : Path.GetFullPath(Path.Combine(CurrentPath ?? throw new ArgumentNullException(CurrentPath), path));

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не найден.");
        }

        string? directory = Path.GetDirectoryName(filePath) ?? throw new ArgumentNullException(filePath);
        string newFilePath = Path.Combine(directory, newName);

        if (File.Exists(newFilePath))
        {
            throw new IOException("Файл уже существует.");
        }

        File.Move(filePath, newFilePath);
    }
}