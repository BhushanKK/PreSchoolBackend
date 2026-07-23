namespace PreSchoolManagement.Domain.Models;
public class PaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchText { get; set; }
    public bool Filter { get; set; } = false;
}