using System.Windows;
using User.ViewModel;

namespace User.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
      
    }
}