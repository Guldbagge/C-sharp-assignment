using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppGraphic.ViewModels;


public partial class DisplayAddViewModel : ObservableObject
{
    private PersonService _personService;

    private readonly IServiceProvider _sp;

    public DisplayAddViewModel(IServiceProvider sp)
    {
        _sp = sp;
        _personService = new PersonService();
    }

    [RelayCommand]
    private void NavigateToList()
    {
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<DisplayMainOptionsViewModel>();
    }

    [RelayCommand]
    public void AddPerson()
    {
        var newPerson = GetPersonDetailsFromUser(null!);

        if (newPerson != null)
        {
            _personService.AddPerson(newPerson);
        }
    }

    private static Person GetPersonDetailsFromUser(string email)
    {
        var person = new Person();

        if (!string.IsNullOrWhiteSpace(person.Email) &&
            !string.IsNullOrWhiteSpace(person.FirstName))
        {
            person = new Person();
        }

        return person;
    }
}
