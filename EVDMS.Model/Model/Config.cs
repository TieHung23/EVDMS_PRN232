using EVDMS.Core.Base;

namespace EVDMS.Core.Model;

public class Config : TIdentity<Guid>, ICreatable, IModifiable, IStatus
{
    public Config()
    {
    }


    public Config(string name, string description) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public required DateTime CreatedAt { get; set; }
    public required string CreatedAtTick { get; set; }
    public required string CreatedBy { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public required string ModifiedAtTick { get; set; }
    public required string ModifiedBy { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsDeleted { get; set; }
}