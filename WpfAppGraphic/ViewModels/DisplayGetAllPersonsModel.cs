using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WpfAppGraphic.ViewModels
{
    public class DisplayGetAllPersonsModel : ObservableObject
   
    {

        private readonly PersonService _personService = new();
        public ObservableCollection<string> DisplayedPersons { get; } = new ObservableCollection<string>();

        public async Task GetAllPersonsAsync()
        {
            var persons = await Task.Run(() => _personService.GetPersons());
            DisplayedPersons.Clear();
            foreach (var person in persons)
            {
                DisplayedPersons.Add($"{person.FirstName} {person.LastName}");
                DisplayedPersons.Add("----------------------------------");
            }
        }

    }

 
}