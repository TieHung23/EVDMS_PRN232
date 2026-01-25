namespace EVDMS.Core.Base;

public interface IModifiable
{
    public DateTime ModifiedAt { get; set; }
    public string ModifiedAtTick { get; set; }
    public string ModifiedBy { get; set; }
}