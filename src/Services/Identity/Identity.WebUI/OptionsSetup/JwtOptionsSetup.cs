﻿namespace Identity.WebUI.OptionsSetup;

public class JwtOptionsSetup(IConfiguration _configuration) 
    : IConfigureOptions<JwtOptions>
{
    private const string _sectionName = "Jwt";

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(_sectionName).Bind(options);
    }
}
