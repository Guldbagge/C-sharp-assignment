using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;

namespace WpfAppGraphic.ViewModels
{
    public partial class DisplayAddViewModel(IServiceProvider sp) : ObservableObject
    {
        private readonly PersonService _personService = new();
        private readonly IServiceProvider _sp = sp ?? throw new ArgumentNullException(nameof(sp));

        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string StreetName { get; set; } = "";
        public string StreetNumber { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string City { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
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
                City, 
                PhoneNumber

            );

            if (newPerson != null)
            {
                _personService.AddPerson(newPerson);
                Message = $"Diver {newPerson.FirstName} {newPerson.LastName} added successfully.";
            }
        }
        private static Person GetPersonDetailsFromUser(string email, string firstName, string lastName, string streetName, string streetNumber, string zipCode, string city, string phoneNumber)
        {
            var person = new Person();

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName) && !string.IsNullOrWhiteSpace(streetName) && !string.IsNullOrWhiteSpace(streetNumber) && !string.IsNullOrWhiteSpace(zipCode) && !string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(phoneNumber))
            {
                if (int.TryParse(streetNumber, out int streetNum) && int.TryParse(zipCode, out int zip) && int.TryParse(phoneNumber, out int phone))
                {
                    person.Email = email;
                    person.FirstName = firstName;
                    person.LastName = lastName;
                    person.StreetName = streetName;
                    person.StreetNumber = streetNum;
                    person.ZipCode = zip;
                    person.City = city;
                    person.PhoneNumber = phone;
                }
            }
            return person;
        }

    }
}