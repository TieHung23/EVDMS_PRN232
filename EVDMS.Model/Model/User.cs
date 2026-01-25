using EVDMS.Core.Base;

namespace EVDMS.Core.Model;

public class User : TIdentity<Guid>, ICreatable, IModifiable, IStatus
{
    public User()
    {

    }

    public User(string userName, string hashedPassword, string fullName) : base(Guid.NewGuid())
    {
        UserName = userName;
        HashedPassword = hashedPassword;
        FullName = fullName;
    }

    public string UserName { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;

    public Guid RoleId { get; set; }

    public Role? Role { get; set; }

    public Guid DealerId { get; set; }

    public Dealer? Dealer { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required string CreatedAtTick { get; set; }
    public required string CreatedBy { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public required string ModifiedAtTick { get; set; }
    public required string ModifiedBy { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsDeleted { get; set; }
}