using System.Windows.Controls;
using WpfAppGraphic.ViewModels;

namespace WpfAppGraphic.Views
{
    public partial class DisplayGetAllPersons : UserControl
    {
        private readonly DisplayGetAllPersonsModel _viewModel;

        public DisplayGetAllPersons()
        {
            InitializeComponent();
            //_viewModel = new DisplayGetAllPersonsModel();
            //DataContext = _viewModel;

            //Loaded += async (_, __) => await _viewModel.GetAllPersonsAsync();
        }
    }
}
