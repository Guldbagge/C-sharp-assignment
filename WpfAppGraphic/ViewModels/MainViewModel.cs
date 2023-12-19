using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace WpfAppGraphic.ViewModels;

public partial class MainViewModel : ObservableObject

{
    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    private readonly IServiceProvider _sp;

    public MainViewModel(IServiceProvider sp)

    {

        _sp = sp;
        CurrentViewModel = _sp.GetRequiredService<DisplayMainOptionsViewModel>();

    }

    
}