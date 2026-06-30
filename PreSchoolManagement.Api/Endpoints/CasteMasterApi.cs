using MediatR;
using SchoolAdmission.Application.Features.CasteMasters.Commands;
using SchoolAdmission.Application.Features.CasteMasters.Queries;
using SchoolAdmission.Domain.Dtos;

namespace SchoolAdmission.Api.Endpoints;

public static class CasteMasterApi
{
    public static IEndpointRouteBuilder MapCasteMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/castemaster");

        group.MapGet("", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetAllCasteMasterQuery();
            return await sender.Send(query, cancellationToken);
        });

        group.MapGet("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetByIdCasteMasterQuery { CasteId = id };
            return await sender.Send(query, cancellationToken);
        });

        group.MapPost("", async (CreateCasteMasterCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            return await sender.Send(command, cancellationToken);
        });

        group.MapPut("/{id:int}", async (int id, CasteMasterCommandDto request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateCasteMasterCommand
            {
                CasteId = id,
                CategoryId = request.CategoryId,
                Caste = request.Caste
            };

            return await sender.Send(command, cancellationToken);
        });

        group.MapDelete("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteCasteMasterCommand { CasteId = id };
            return await sender.Send(command, cancellationToken);
        });

        return app;
    }
}
