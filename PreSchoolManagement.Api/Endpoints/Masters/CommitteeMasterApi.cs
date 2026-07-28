using MediatR;
using Microsoft.AspNetCore.Mvc;
using PreSchoolManagement.Api.HttpRequests;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Application.Features.Queries;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Api.Endpoints;

public static class CommitteeMasterApi
{
    public static IEndpointRouteBuilder MapCommitteeMasterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/CommitteeMaster")
                        .WithTags("Committee Master");

        group.MapGet("/", GetAll)
            .WithName("GetAllCommittee")
            .WithSummary("Get all committee master records with pagination.")
            .WithDescription("Return all committee master record with pagination")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        group.MapGet("/{id:guid}", GetById)
            .WithName("GetCommitteeById")
            .WithSummary("Get committee by Id")
            .WithDescription("Returns a committee record by Id.")
            .RequireAuthorization();

        group.MapPost("/", Create)
            .DisableAntiforgery()
            .WithName("CreateCommittee")
            .WithSummary("Create Committee")
            .WithDescription("Creates a new committee master record.")
            .RequireAuthorization();

        group.MapPut("/{id:guid}", Update)
            .DisableAntiforgery()
            .WithName("UpdateCommittee")
            .WithSummary("Update Committee ")
            .WithDescription("Updates an existing committee master record.")
            .RequireAuthorization();

        group.MapDelete("/{id:guid}", Delete)
            .WithName("DeleteCommittee")
            .WithSummary("Delete Committee")
            .WithDescription("Deletes a committee master record.")
            .RequireAuthorization();
        return app;
    }

    private static async Task<IResult> GetAll(
        [AsParameters] PaginationRequest request, 
        ISender sender, 
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllCommitteeMasterQuery(request), cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetById(ISender sender, CancellationToken cancellationToken, Guid id)
    {
        var result = await sender.Send(new GetByIdCommitteeMasterQuery(id),
        cancellationToken);
        return TypedResults.Ok(result);
    }
    private static async Task<IResult> Create([FromForm] CreateCommitteeMasterRequest request,
    ISender sender,IFileStorageService fileStorage,CancellationToken cancellationToken)
    {
        string? logo = null;
        string? logoPath = null;

        if (request.Logo is not null)
        {
            await using var stream = request.Logo.OpenReadStream();
            var upload = await fileStorage.SaveAsync(stream,request.Logo.FileName, "committee", cancellationToken);
            logo = upload.FileName;
            logoPath = upload.FilePath;
        }

        var command = new CreateCommitteeMasterCommand
        {
            CommitteeName = request.CommitteeName,
            IsActive = request.IsActive,
            Slogan = request.Slogan,
            Logo = logo,
            LogoPath = logoPath
        };

        var result =await sender.Send(command,cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Update(Guid id,[FromForm] UpdateCommitteeMasterRequest request,
        ISender sender,IFileStorageService fileStorage, CancellationToken cancellationToken)
    {
        string? logo = null;
        string? logoPath = null;
        if (request.Logo is not null)
        {
            await using var stream = request.Logo.OpenReadStream();
            var upload = await fileStorage.SaveAsync(stream,request.Logo.FileName,"committee",cancellationToken);
            logo = upload.FileName;
            logoPath = upload.FilePath;
        }

        var command = new UpdateCommitteeMasterCommand
        {
            CommitteeId = id,
            CommitteeName = request.CommitteeName,
            IsActive = request.IsActive,
            Slogan = request.Slogan,
            Logo = logo,
            LogoPath = logoPath
        };

        var result = await sender.Send(command,cancellationToken);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> Delete(Guid id,ISender sender,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteCommitteeMasterCommand(id),cancellationToken);
        return TypedResults.Ok(result);
    }
}