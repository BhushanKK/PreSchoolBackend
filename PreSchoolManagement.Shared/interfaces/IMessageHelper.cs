namespace PreSchoolManagement.Shared.Common;

public interface IMessageHelper
{
    string Added(string entityName);
    string Updated(string entityName);
    string Deleted(string entityName);
    string Retrieved(string entityName);
    string AlreadyExists(string entityName);
    string NotFound(string entityName);
    string InvalidId(string entityName);
    string Entity(string resourceName, string key);

    string AddedEntity(string resourceName, string key); 
    string UpdatedEntity(string resourceName, string key); 
    string DeletedEntity(string resourceName, string key); 
    string RetrievedEntity(string resourceName, string key); 
    string AlreadyExistsEntity(string resourceName, string key); 
    string NotFoundEntity(string resourceName, string key); 
    string InvalidIdEntity(string resourceName, string key);
}