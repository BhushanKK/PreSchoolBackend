using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;

namespace PreSchoolManagement.Api.Endpoints;
public static class EmployeeTypeMasterApi
{
    public static IEndpointRouteBuilder MapEmployeeTypeMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/EmployeeTypeMaster")
                        .WithTags("Employee Type Master");

        group.MapGet("/{filter:bool}", GetAll)
            .WithName("GetAllEmployeer")
            .WithSummary("Get all employee type master records.")
            .WithDescription("Return all employee type master record")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetEmployeeTypeById")
            .WithSummary("Get employee type  by Id")
            .WithDescription("Returns a employee type master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateEmployeeType")
            .WithSummary("Create Employee Type")
            .WithDescription("Creates a new employee master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}",Update)
            .WithName("UpdateEmployee")
            .WithSummary("Update Employee ")
            .WithDescription("Updates an existing employee type master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteEmployee")
            .WithSummary("Delete Employee Type")
            .WithDescription("Deletes a employee type master record.")
            .RequireAuthorization();
        
        return app;

    }

    private static async Task<IResult> Delete(int id,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteEmployeeTypeMasterCommand(id), cancellationToken);
        return TypedResults.Ok(result);

    }   

    private static async Task<IResult> Create(CreateEmployeeTypeMasterCommand command,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(command,cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(ISender sender,CancellationToken cancellationToken,int id)
    {
        var result = await sender.Send(new GetByIdEmployeeTypeMasterQuery(id),
        cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetAll(bool filter,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender. Send(new GetAllEmployeeTypeMasterQuery(filter),cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult>Update(int id,UpdateEmployeeTypeMasterCommand request,ISender sender,CancellationToken cancellationToken)
    {
        request.EmployeeTypeId =id;
        var result = await sender.Send(request,cancellationToken);
        return TypedResults.Ok(result);
    }
}   