using CommunityToolkit.Mvvm.ComponentModel;
using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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