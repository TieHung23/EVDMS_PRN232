namespace EVDMS.Core.Base;

public interface IStatus
{
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}