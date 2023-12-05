using Microsoft.Extensions.Hosting; 
using ConsoleApp.Service; 
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateDefaultBuilder().ConfigureServices(service =>
{
    service.AddSingleton<PersonService>(); 
    service.AddSingleton<MenuService>(); 
}).Build(); 

builder.Start(); 
Console.Clear(); 

var menuService = builder.Services.GetRequiredService<MenuService>(); // Get MenuService from services

menuService.ShowMainMenu(); 
