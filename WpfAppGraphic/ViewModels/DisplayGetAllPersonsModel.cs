using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WpfAppGraphic.ViewModels
{
    public class DisplayGetAllPersonsModel : ObservableObject
    {
        private readonly IServiceProvider _sp;
        public ObservableCollection<string> DisplayedPersons { get; } = new ObservableCollection<string>();

        public ICommand NavigateToListCommand { get; }

        public DisplayGetAllPersonsModel(IServiceProvider sp)
        {
            _sp = sp ?? throw new ArgumentNullException(nameof(sp));
            NavigateToListCommand = new RelayCommand(NavigateToList);

            RefreshData();
        }

        private void RefreshData()
        {
            // Hämta PersonService från ServiceProvider
            var personService = _sp.GetRequiredService<PersonService>();
            var persons = personService.GetPersons();

            DisplayedPersons.Clear();
            foreach (var person in persons)
            {
                DisplayedPersons.Add($"{person.FirstName} {person.LastName}");
            }
        }

        private void NavigateToList()
        {
            var mainViewModel = _sp.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _sp.GetRequiredService<DisplayMainOptionsViewModel>();
        }
    }
}
