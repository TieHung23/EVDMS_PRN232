using System.ComponentModel.DataAnnotations;

namespace EVDMS.Core.Base;

public abstract class TIdentity<Tid>
{
    [Key]
    public required Tid Id { get; set; }

    protected TIdentity()
    {
    }

    protected TIdentity(Tid id)
    {
        Id = id;
    }
}