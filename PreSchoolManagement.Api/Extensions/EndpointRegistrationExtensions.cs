namespace PreSchoolManagement.Api.Endpoints;

public static class EndpointRegistrationExtensions
{
    public static IEndpointRouteBuilder MapApplicationEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapAuthEndpoints();
        app.MapCasteMasterEndpoints();
        app.MapReligionMasterEndpoints();
        app.MapAcademicYearMasterEndpoints();
        app.MapFinancialYearMasterEndpoints();

        return app;
    }
}