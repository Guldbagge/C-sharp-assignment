using Microsoft.Extensions.Hosting; 
using ConsoleApp.Service; 
using Microsoft.Extensions.DependencyInjection;
using Shared.Service;

var builder = Host.CreateDefaultBuilder().ConfigureServices(service =>
{
    service.AddSingleton<PersonService>(); 
    service.AddSingleton<MenuService>(); 
}).Build(); 

builder.Start(); 
Console.Clear(); 

var menuService = builder.Services.GetRequiredService<MenuService>();

menuService.ShowMainMenu(); 
