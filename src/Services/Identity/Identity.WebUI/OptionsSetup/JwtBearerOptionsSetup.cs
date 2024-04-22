﻿using Identity.Application.Authentication;

namespace Identity.WebUI.OptionsSetup;

public class JwtBearerOptionsSetup(IOptions<JwtOptions> _jwtOptions)
    : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions = _jwtOptions.Value;

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
        };
    }

    public void Configure(string? name, JwtBearerOptions options) => Configure(options);
}