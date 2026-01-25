using EVDMS.Core.Base;

namespace EVDMS.Core.Model;

public class Dealer : TIdentity<Guid>, ICreatable, IModifiable, IStatus
{
    public Dealer()
    {
    }


    public Dealer(string code, string name, string email) : base(Guid.NewGuid())
    {
        Code = code;
        Name = name;
        Email = email;
    }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public required DateTime CreatedAt { get; set; }
    public required string CreatedAtTick { get; set; }
    public required string CreatedBy { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public required string ModifiedAtTick { get; set; }
    public required string ModifiedBy { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsDeleted { get; set; }
}