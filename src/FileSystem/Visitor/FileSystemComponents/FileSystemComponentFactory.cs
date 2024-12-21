namespace Itmo.ObjectOrientedProgramming.Lab4.Visitor.FileSystemComponents;

public class FileSystemComponentFactory
{
    public IFileSystemComponent Create(string path, int depth)
    {
        if (Directory.Exists(path))
        {
            string name = Path.GetFileName(path) ?? path;

            if (depth == 0)
            {
                return new DirectoryFileSystemComponent(name, new List<IFileSystemComponent>());
            }

            IEnumerable<string> entries = Directory.EnumerateFileSystemEntries(path);

            IFileSystemComponent[] components = entries
                .Select(entry => Create(entry, depth - 1))
                .ToArray();

            return new DirectoryFileSystemComponent(name, components);
        }

        if (File.Exists(path))
        {
            string name = Path.GetFileName(path);
            return new FileFileSystemComponent(name);
        }

        throw new IOException($"Компонент файловой системы по адресу {path} не найден");
    }
}