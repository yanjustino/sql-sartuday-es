// Global using directives

global using Bogus;
global using Domain.Adapters.Repositories;
global using FluentAssertions;
global using Xunit;
global using Domain.Models;
global using Domain.UseCases.AddMovement;
global using IntegratedTests.Commons;
global using Infrastructure.Database;
global using Infrastructure.Database.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection;
global using Testcontainers.MsSql;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.Extensions.Configuration;

global using static Domain.Models.MovementType;
