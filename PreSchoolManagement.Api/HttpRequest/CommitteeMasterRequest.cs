namespace PreSchoolManagement.Api.HttpRequests;
public class CreateCommitteeMasterRequest : BaseCommitteeMasterRequest;
public class UpdateCommitteeMasterRequest : BaseCommitteeMasterRequest;
public class BaseCommitteeMasterRequest
{
    public string CommitteeName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? Slogan { get; set; }
    public IFormFile? Logo { get; set; }
}