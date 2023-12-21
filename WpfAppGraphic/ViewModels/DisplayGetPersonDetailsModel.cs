using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

namespace WpfAppGraphic.ViewModels
{
    public partial class DisplayGetPersonDetailsModel : ObservableObject
    {
        private readonly PersonService _personService;
        private readonly IServiceProvider _sp;

        public DisplayGetPersonDetailsModel(IServiceProvider sp)
        {
            _sp = sp ?? throw new ArgumentNullException(nameof(sp));
            _personService = new PersonService();
            NavigateToListCommand = new RelayCommand(NavigateToList);
            GetPersonDetailsCommand = new RelayCommand(GetPersonDetails);
            _email = string.Empty; 
            _personDetails = string.Empty; 
        }

        public ICommand NavigateToListCommand { get; }
        public ICommand GetPersonDetailsCommand { get; }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _personDetails = string.Empty;
        public string PersonDetails
        {
            get => _personDetails;
            set => SetProperty(ref _personDetails, value);
        }

        private void NavigateToList()
        {
            Email = string.Empty;
            var mainViewModel = _sp.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _sp.GetRequiredService<DisplayMainOptionsViewModel>();
        }

        private void GetPersonDetails()
        {
            string emailInput = Email;

            var result = _personService.GetPerson(emailInput);

            if (result != null)
            {
                DisplayPersonInformation(result);
            }
            else
            {
                PersonDetails = "Person not found.";
            }
        }

        private void DisplayPersonInformation(Person person)
        {
            PersonDetails = $"\nPerson Information:\n----------------------------------\n" +
                            $"Email: {person.Email}\n" +
                            $"First Name: {person.FirstName}\n" +
                            $"Last Name: {person.LastName}\n" +
                            $"Address:\n\tStreet Name: {person.StreetName}\n" +
                            $"\tStreet Number: {person.StreetNumber}\n" +
                            $"\tZip Code: {person.ZipCode}\n" +
                            $"\tCity: {person.City}\n" +
                            $"Phone Number: {person.PhoneNumber}\n" +
                            $"----------------------------------\n\n";
        }
    }
}
