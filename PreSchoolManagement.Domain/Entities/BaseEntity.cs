namespace SchoolManagement.Domain;

public abstract class BaseEntity
{
    public int? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public int? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }
}