using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class LaboratoryRepository
{
    private static readonly Lazy<LaboratoryRepository> Lazy;

    public static LaboratoryRepository Instance => Lazy.Value;

    private readonly Dictionary<Guid, Laboratory> _laboratories;

    static LaboratoryRepository()
    {
        Lazy = new Lazy<LaboratoryRepository>(() => new LaboratoryRepository());
    }

    private LaboratoryRepository()
    {
        _laboratories = new Dictionary<Guid, Laboratory>();
    }

    public void Add(Laboratory laboratory)
    {
        _laboratories[laboratory.LaboratoryId] = laboratory;
    }

    public Laboratory? GetById(Guid laboratoryId)
    {
        _laboratories.TryGetValue(laboratoryId, out Laboratory? laboratory);
        return laboratory;
    }
}