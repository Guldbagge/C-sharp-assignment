using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WpfAppGraphic.ViewModels
{
    public partial class DisplayRemovePersonModel : ObservableObject
    {
        private readonly PersonService _personService;
        private readonly IServiceProvider _sp;

        public DisplayRemovePersonModel(IServiceProvider sp)
        {
            _sp = sp ?? throw new ArgumentNullException(nameof(sp));
            _personService = new PersonService();
            NavigateToListCommand = new RelayCommand(NavigateToList);
            RemovePersonCommand = new RelayCommand(RemovePerson);
        }

        public RelayCommand NavigateToListCommand { get; }
        public RelayCommand RemovePersonCommand { get; }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private void NavigateToList()
        {
            var mainViewModel = _sp.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _sp.GetRequiredService<DisplayMainOptionsViewModel>();
        }

        private void RemovePerson()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                Message = "Please enter a valid email.";
                return;
            }

            var result = _personService.RemovePerson(Email);

            if (!result)
            {
                Message = "Person not found. Cannot remove.";
            }
            else
            {
                Message = "Person removed successfully.";
            }
        }
    }
}
