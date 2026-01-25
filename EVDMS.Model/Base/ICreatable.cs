namespace EVDMS.Core.Base;

public interface ICreatable
{
    public DateTime CreatedAt { get; set; }
    public string CreatedAtTick { get; set; }
    public string CreatedBy { get; set; }
}