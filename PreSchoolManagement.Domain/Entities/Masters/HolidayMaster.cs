namespace SchoolManagement.Domain.Entities;

public class HolidayMaster : BaseEntity
{
    public int HolidayId { get; set; }

    public string HolidayName { get; set; } = string.Empty;

    public DateTime HolidayDate { get; set; }

    public string? HolidayType { get; set; }

    public string? Description { get; set; }
}
