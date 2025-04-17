using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UserService.Application.Contracts.AuthenticationContracts;
using UserService.Application.DTO;
using UserService.Application.Exceptions;
using UserService.Domain.Models;

namespace UserService.Infrastructure;

public class AuthenticationManager(
    UserManager<User> userManager,
    IConfiguration configuration,
    ITokenService tokenService) : IAuthenticationManager
{
    public async Task<User> ValidateUser(AuthenticateUserDto userForAuth)
    {
        var user = await userManager.FindByNameAsync(userForAuth.UserName);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Cannot find user");
        }

        if (!await userManager.CheckPasswordAsync(user, userForAuth.Password))
        {
            throw new UnauthorizedAccessException("Invalid password");
        }

        return user;
    }

    public async Task<TokenDto> CreateTokens(User user, bool populateExp)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var refreshTokenLifeTimeStr = jwtSettings.GetSection("RefreshTokenLifeTime").Value;
        if (refreshTokenLifeTimeStr is null)
            throw new UnauthorizedAccessException("RefreshTokenLifeTime is null");
        var refreshTokenLifeTime = int.Parse(refreshTokenLifeTimeStr);
        
        var signingCredentials = tokenService.GetSigningCredentials();
        var claims = await tokenService.GetClaims(user);
        var tokenOptions = tokenService.GenerateTokenOptions(signingCredentials, claims);
        
        var refreshToken = tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        
        if (populateExp)
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddMinutes(refreshTokenLifeTime);
        await userManager.UpdateAsync(user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        
        return new TokenDto(accessToken, refreshToken);
    }

    public async Task<string> CreateAccessToken(User user)
    {
        var signingCredentials = tokenService.GetSigningCredentials();
        var claims = await tokenService.GetClaims(user);
        var tokenOptions = tokenService.GenerateTokenOptions(signingCredentials, claims);
        
        await userManager.UpdateAsync(user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        
        return accessToken;
    }
    
    public async Task Logout(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            throw new NotFoundException("User not found.");

        user.RefreshToken = null;
        await userManager.UpdateAsync(user);
    }
    
}