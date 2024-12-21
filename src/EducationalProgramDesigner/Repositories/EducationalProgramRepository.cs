using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class EducationalProgramRepository
{
    private static readonly Lazy<EducationalProgramRepository> Lazy;

    public static EducationalProgramRepository Instance => Lazy.Value;

    private readonly Dictionary<Guid, EducationalProgram> _educationalPrograms;

    static EducationalProgramRepository()
    {
        Lazy = new Lazy<EducationalProgramRepository>(() => new EducationalProgramRepository());
    }

    private EducationalProgramRepository()
    {
        _educationalPrograms = new Dictionary<Guid, EducationalProgram>();
    }

    public void Add(EducationalProgram program)
    {
        _educationalPrograms[program.EducationalProgramId] = program;
    }

    public EducationalProgram? GetById(Guid programId)
    {
        _educationalPrograms.TryGetValue(programId, out EducationalProgram? program);
        return program;
    }
}