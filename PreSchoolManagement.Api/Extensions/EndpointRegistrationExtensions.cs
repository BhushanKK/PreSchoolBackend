namespace PreSchoolManagement.Api.Endpoints;

public static class EndpointRegistrationExtensions
{
    public static IEndpointRouteBuilder MapApplicationEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapAuthEndpoints();
        app.MapCategoryMasterEndpoints();
        app.MapCasteMasterEndpoints();
        app.MapReligionMasterEndpoints();
        app.MapAcademicYearMasterEndpoints();
        app.MapFinancialYearMasterEndpoints();
        app.MapRoleMasterEndpoints();
        app.MapSectionMasterEndpoints();  
        app.MapDivisionMasterEndpoints(); 
        app.MapStandardMasterEndpoints(); 
        app.MapMenuMasterEndpoints();
        app.MapRoleMenuPermissionEndpoints();
        return app;
    }
}