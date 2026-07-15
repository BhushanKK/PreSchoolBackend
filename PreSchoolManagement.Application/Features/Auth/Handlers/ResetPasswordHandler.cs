using MediatR;
using PreSchoolManagement.Application.Features.Auth.Commands;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Auth.Handlers;

public class ResetPasswordHandler(
    IPasswordResetService passwordRepository,
    IAuthService authService)
    : IRequestHandler<ResetPasswordCommand, bool>
{
    public async Task<bool> Handle(
        ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var resetToken = await passwordRepository.GetByTokenAsync(request.Token);

        if (resetToken == null)
            throw new Exception("Invalid password reset token.");

        if (resetToken.IsUsed)
            throw new Exception("This password reset link has already been used.");

        if (resetToken.ExpiryDate < DateTime.UtcNow)
            throw new Exception("This password reset link has expired.");

        var user = resetToken.User;

        if (user == null)
            throw new Exception("User not found.");

        // Hash password
        await authService.ResetPasswordAsync(user,request.Password,cancellationToken);
        
        // Update user password
        await authService.UpdateUserAsync(user, cancellationToken);

        // Mark token as used
        resetToken.IsUsed = true;
        resetToken.UsedDate = DateTime.UtcNow;

        await passwordRepository.UpdateAsync(resetToken);

        return true;
    }
}