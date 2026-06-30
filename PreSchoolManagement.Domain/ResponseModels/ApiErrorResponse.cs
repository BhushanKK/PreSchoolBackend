namespace SchoolAdmission.Domain.ResponseModels;
public class ApiErrorResponse
{
    public string? Message { get; set; }
    public string? Details { get; set; }
    public string? TraceId { get; set; }
}
