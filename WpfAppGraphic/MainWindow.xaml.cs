using System.Windows;
using WpfAppGraphic.ViewModels;

namespace WpfAppGraphic;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;

    }
}