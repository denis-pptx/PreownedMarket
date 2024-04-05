global using Identity.Application.Abstractions.Messaging;
global using Identity.Application.Abstractions;
global using Identity.Application.Exceptions;
global using Identity.Application.Exceptions.ErrorMessages;
global using Identity.Application.Features.Identity.Commands.RegisterUser;
global using Identity.Application.Features.Users.Queries.Models;
global using Identity.Application.Models;
global using Identity.Domain.Models;
global using Identity.Domain.Enums;

global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Logging;
global using Microsoft.EntityFrameworkCore;
global using System.Security.Claims;

global using MediatR;
global using AutoMapper;
global using Serilog.Context;


