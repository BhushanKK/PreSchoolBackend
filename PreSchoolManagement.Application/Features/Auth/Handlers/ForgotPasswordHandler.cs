using System.Security.Cryptography;
using MediatR;
using Microsoft.Extensions.Options;
using PreSchoolManagement.Application.Features.Auth.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Shared.Constants;

namespace PreSchoolManagement.Application.Features.Auth.Handlers;

public class ForgotPasswordHandler(
    IAuthService authService,
    IPasswordResetService passwordResetService,
    IEmailService emailService,
    IEmailTemplateService emailTemplateService,
    IOptions<FrontendSettings> frontendOptions)
    : IRequestHandler<ForgotPasswordCommand, bool>
{
    private readonly FrontendSettings _frontendSettings = frontendOptions.Value;

    public async Task<bool> Handle(
        ForgotPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await authService.GetUserByEmailAsync(
            request.Email,
            cancellationToken);

        // Prevent email enumeration
        if (user is null)
            return true;

        // Invalidate any existing active reset tokens
        await passwordResetService.InvalidateTokensAsync(user.UserId);

        // Generate secure token
        var token = Convert.ToHexString(
            RandomNumberGenerator.GetBytes(32));

        var passwordResetToken = new PasswordResetToken
        {
            UserId = user.UserId,
            Token = token,
            CreatedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddMinutes(30),
            IsUsed = false
        };

        await passwordResetService.CreateAsync(passwordResetToken);

        var resetLink =
            $"{_frontendSettings.BaseUrl}/reset-password?token={token}";

        var html = await emailTemplateService.GetForgotPasswordTemplateAsync(
            user.UserName,
            resetLink);

        await emailService.SendAsync(
            user.Email,
            "Reset Your GurukulX Password",
            html);

        return true;
    }
}