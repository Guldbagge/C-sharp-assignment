using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace WpfAppGraphic.ViewModels;

public partial class DisplayMainOptionsViewModel : ObservableObject
{
private readonly IServiceProvider _sp;

public DisplayMainOptionsViewModel(IServiceProvider sp)
{
    _sp = sp;
}

    [RelayCommand]
    private void NavigateToAdd()
    {
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<DisplayAddViewModel>();
    }

    [RelayCommand]
    private void NavigateToRemovePerson()
    {
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<DisplayRemovePersonModel>();
    }

    [RelayCommand]
    private void NavigateToGetAllPersons()
    {
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<DisplayGetAllPersonsModel>();
    }

    [RelayCommand]
    private void NavigateToGetPersonDetails()
    {
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<DisplayGetPersonDetailsModel>();
    }

    [RelayCommand]
    private void NavigateToUpdatePerson()
    {
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<DisplayUpdatePersonModel>();
    }



}