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

public partial class DisplayUpdatePersonModel(IServiceProvider sp) : ObservableObject
    {
    //private readonly PersonService _personService = new();

    [RelayCommand]
    private void NavigateToList()
    {
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<DisplayMainOptionsViewModel>();
    }
    private readonly IServiceProvider _sp = sp ?? throw new ArgumentNullException(nameof(sp));
}
