using EVDMS.Core.Base;

namespace EVDMS.Core.Model;

public class Role : TIdentity<Guid>
{
    public Role()
    {
    }


    public Role(Guid id, string name, string description) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set;  } = string.Empty;

    public string Description { get; set;  } = string.Empty;
}