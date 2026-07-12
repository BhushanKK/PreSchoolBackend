using MediatR;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;

namespace PreSchoolManagement.Api.Endpoints;

public static class CategoryMasterApi
{
    public static IEndpointRouteBuilder MapCategoryMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/categorymaster")
                       .WithTags("Category Master");

        group.MapGet("/{filter:bool}", GetAll)
            .WithName("GetAllCategories")
            .WithSummary("Get all category masters")
            .WithDescription("Returns all category master records.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName("GetCategoryById")
            .WithSummary("Get category by Id")
            .WithDescription("Returns a category master record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .WithName("CreateCategory")
            .WithSummary("Create category")
            .WithDescription("Creates a new category master record.")
            .RequireAuthorization();

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateCategory")
            .WithSummary("Update category")
            .WithDescription("Updates an existing category master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteCategory")
            .WithSummary("Delete category")
            .WithDescription("Deletes a category master record.")
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetAll(bool filter,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllCategoryMasterQuery(filter),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetByIdCategoryMasterQuery(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Create(
        CreateCategoryMasterCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            command,
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Update(
        int id,
        UpdateCategoryMasterCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        request.CategoryId = id;

        var result = await sender.Send(
            request,
            cancellationToken);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Delete(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteCategoryMasterCommand(id),
            cancellationToken);

        return TypedResults.Ok(result);
    }
}