namespace SchoolManagement.Domain.Entities;

public class CasteMaster : BaseEntity
{
    public int CasteID { get; set; }

    public int CategoryID { get; set; }

    public string CasteName { get; set; } = string.Empty;
}
