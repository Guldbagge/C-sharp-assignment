using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace WpfAppGraphic.ViewModels
{
    public partial class DisplayUpdatePersonModel : ObservableObject
    {
        private readonly PersonService _personService = new();
        private readonly IServiceProvider _sp;

        public DisplayUpdatePersonModel(IServiceProvider sp)
        {
            _sp = sp ?? throw new ArgumentNullException(nameof(sp));
        }

        private RelayCommand _navigateToListCommand;
        public RelayCommand NavigateToListCommand =>
            _navigateToListCommand ??= new RelayCommand(NavigateToList);

        private RelayCommand _updatePersonCommand;
        public RelayCommand UpdatePersonCommand =>
            _updatePersonCommand ??= new RelayCommand(UpdatePerson);

        private void NavigateToList()
        {
            var mainViewModel = _sp.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _sp.GetRequiredService<DisplayMainOptionsViewModel>();
        }

        private void UpdatePerson()
        {
            string personEmailInput = Microsoft.VisualBasic.Interaction.InputBox("Enter person email to update:", "Input", "");

            var personToUpdate = _personService.GetPerson(personEmailInput);

            if (personToUpdate == null)
            {
                MessageBox.Show("Person not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var response = MessageBox.Show("Do you want to update the email?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (response == MessageBoxResult.Yes)
            {
                UpdatePersonEmail(personToUpdate);
                MessageBox.Show("Email updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            UpdatePersonDetails(personToUpdate);
            _personService.UpdatePerson(personToUpdate);
            MessageBox.Show("Person details updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdatePersonEmail(Person person)
        {
            string newEmail = Microsoft.VisualBasic.Interaction.InputBox("Enter new person email:", "Input", "");
            if (!string.IsNullOrEmpty(newEmail))
            {
                person.Email = newEmail;
            }
        }

        private void UpdatePersonDetails(Person person)
        {
            person.FirstName = Microsoft.VisualBasic.Interaction.InputBox("Enter person first name:", "Input", "");
            person.LastName = Microsoft.VisualBasic.Interaction.InputBox("Enter person last name:", "Input", "");
            person.StreetName = Microsoft.VisualBasic.Interaction.InputBox("Enter person street name:", "Input", "");

            int streetNumber;
            if (int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Enter person street number:", "Input", ""), out streetNumber))
            {
                person.StreetNumber = streetNumber;
            }

            int zipCode;
            if (int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Enter person zip code:", "Input", ""), out zipCode))
            {
                person.ZipCode = zipCode;
            }

            person.City = Microsoft.VisualBasic.Interaction.InputBox("Enter person city:", "Input", "");


            int phoneNumber;
            if (int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Enter person phone number:", "Input", ""), out phoneNumber))
            {
                person.PhoneNumber = phoneNumber;
            }
        }
    }
}
