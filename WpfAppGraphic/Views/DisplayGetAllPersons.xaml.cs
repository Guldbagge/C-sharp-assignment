using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppGraphic.ViewModels;

//namespace WpfAppGraphic.Views
//{
//    /// <summary>
//    /// Interaction logic for DisplayGetAllPersons.xaml
//    /// </summary>
//    public partial class DisplayGetAllPersons : UserControl
//    {
//        public DisplayGetAllPersons()
//        {
//            InitializeComponent();
//        }
//    }
//}


namespace WpfAppGraphic.Views
{
    public partial class DisplayGetAllPersons : UserControl
    {
        private readonly DisplayGetAllPersonsModel _viewModel;

        public DisplayGetAllPersons()
        {
            InitializeComponent();
            _viewModel = new DisplayGetAllPersonsModel();
            DataContext = _viewModel;

            Loaded += async (_, __) => await _viewModel.GetAllPersonsAsync();
        }
    }
}
