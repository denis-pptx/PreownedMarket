global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.WebUtilities;

global using Identity.Application.Features.Identity.Commands.LoginUser;
global using Identity.Application.Features.Identity.Commands.RefreshToken;
global using Identity.Application.Features.Identity.Commands.RegisterUser;
global using Identity.Application.Exceptions;
global using Identity.Infrastructure.Authentication;

global using MediatR;