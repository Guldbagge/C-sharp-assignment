using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppGraphic.ViewModels
{
    public partial class DisplayAddViewModel : ObservableObject
    {
        private PersonService _personService;
        private readonly IServiceProvider _sp;

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
            var newPerson = GetPersonDetailsFromUser(
                Email,
                FirstName,
                LastName,
                StreetName,
                StreetNumber,
                ZipCode,
                City
            );

            if (newPerson != null)
            {
                _personService.AddPerson(newPerson);
            }
        }

        private static Person GetPersonDetailsFromUser(string email, string firstName, string lastName, string streetName, int streetNumber, int zipCode, string city)
        {
            var person = new Person();

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName) && !string.IsNullOrWhiteSpace(streetName) && streetNumber != 0 && zipCode != 0 && !string.IsNullOrWhiteSpace(city))
            {
                person.Email = email;
                person.FirstName = firstName;
                person.LastName = lastName;
                person.StreetName = streetName;
                person.StreetNumber = streetNumber;
                person.ZipCode = zipCode;
                person.City = city;
            }

            return person;
        }
    }
}
