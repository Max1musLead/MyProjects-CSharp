using Itmo.ObjectOrientedProgramming.Lab4.Outputs;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

public interface IFileSystem
{
    string? CurrentPath { get; }

    void Connect(string address);

    void Disconnect();

    void ChangeDirectory(string path);

    void GetDirectoryTree(int depth);

    void ShowFile(string path, IOutput output);

    void MoveFile(string sourcePath, string destinationPath);

    void CopyFile(string sourcePath, string destinationPath);

    void DeleteFile(string path);

    void RenameFile(string path, string newName);
}