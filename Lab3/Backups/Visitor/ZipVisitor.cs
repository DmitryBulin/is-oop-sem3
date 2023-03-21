using System.IO.Compression;
using Backups.Archiver;
using Backups.Repository;

namespace Backups.Visitor;

public class ZipVisitor : IRepositoryObjectVisitor
{
    private readonly ZipArchive _archive;

    public ZipVisitor(ZipArchive archive)
    {
        _archive = archive;
        Links = new Stack<List<IZipObject>>();
        Links.Push(new List<IZipObject>());
    }

    public Stack<List<IZipObject>> Links { get; }

    public void Visit(IFile file)
    {
        ZipArchiveEntry entry = _archive.CreateEntry(file.Name);
        Stream entryStream = entry.Open();
        file.Open().CopyTo(entryStream);
        entryStream.Close();
        Links.Peek().Add(new Archiver.ZipFile(file.Name));
    }

    public void Visit(IFolder folder)
    {
        _archive.CreateEntry(folder.Name + "\\");

        Links.Push(new List<IZipObject>());

        foreach (IRepositoryObject item in folder.SubObjects)
        {
            item.Accept(this);
        }

        var zipFolder = new ZipFolder(folder.Name + "\\", Links.Pop());
        Links.Peek().Add(zipFolder);
    }
}
