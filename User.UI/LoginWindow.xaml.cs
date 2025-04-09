using System;
using System.Windows;
using System.Windows.Controls;
using User.ViewModel;
using User.UI;

namespace User.UI
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            _viewModel.LoginSuccessful += ViewModel_LoginSuccessful;
            DataContext = _viewModel;
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = passwordBox.Password; // Sincronizează parola
        }

        private void ViewModel_LoginSuccessful(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}