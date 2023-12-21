using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using WpfAppGraphic.ViewModels;
using WpfAppGraphic.Views;

namespace WpfAppGraphic;

public partial class App : Application
{
    private IHost? _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
              
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<DisplayMainOptionsViewModel>();
                services.AddSingleton<DisplayMainOptions>();
                services.AddSingleton<DisplayAddViewModel>();
                services.AddSingleton<DisplayAdd>();
                services.AddTransient<DisplayGetAllPersonsModel>();
                services.AddTransient<DisplayGetAllPersons>();
                services.AddSingleton<DisplayGetPersonDetailsModel>();
                services.AddSingleton<DisplayGetPersonDetails>();
                services.AddTransient<DisplayRemovePersonModel>();
                services.AddTransient<DisplayRemovePerson>();
                services.AddSingleton<DisplayUpdatePersonModel>();
                services.AddSingleton<DisplayUpdatePerson>();
                services.AddTransient<PersonService>();


            })
            .Build();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host?.Start();

        var mainWindow = _host!.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
