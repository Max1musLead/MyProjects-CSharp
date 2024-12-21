using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class LectureMaterialRepository
{
    private static readonly Lazy<LectureMaterialRepository> Lazy;

    public static LectureMaterialRepository Instance => Lazy.Value;

    private readonly Dictionary<Guid, LectureMaterial> _lectureMaterials;

    static LectureMaterialRepository()
    {
        Lazy = new Lazy<LectureMaterialRepository>(() => new LectureMaterialRepository());
    }

    private LectureMaterialRepository()
    {
        _lectureMaterials = new Dictionary<Guid, LectureMaterial>();
    }

    public void Add(LectureMaterial lectureMaterial)
    {
        _lectureMaterials[lectureMaterial.LectureId] = lectureMaterial;
    }

    public LectureMaterial? GetById(Guid lectureId)
    {
        _lectureMaterials.TryGetValue(lectureId, out LectureMaterial? lectureMaterial);
        return lectureMaterial;
    }
}