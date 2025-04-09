using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using User.Services; // For DataService

namespace User.ViewModel
{
    // The LoginViewModel class implements login logic and property change notification in MVVM
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private readonly DataService _dataService;

        // Property for the username
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        // Property for the password
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        // Property for displaying the error message
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        // Command for the login action
        public ICommand LoginCommand { get; }

        // Event triggered when login is successful
        public event EventHandler LoginSuccessful;

        // Constructor to initialize the data service and login command
        public LoginViewModel()
        {
            _dataService = new DataService();
            // Initialize login command with a call to the Login method
            LoginCommand = new RelayCommand(() => Login());
        }

        // Login method
        private void Login()
        {
            // Validate the user using the data service
            var user = _dataService.ValidateUser(Username, Password);

            // If the user is valid, trigger the success event
            if (user != null)
            {
                LoginSuccessful?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                // Display an error message if authentication fails
                ErrorMessage = "Login failed. Please try again.";
            }
        }

        // Event for notifying property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to trigger the PropertyChanged event
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // The RelayCommand class implements the ICommand interface
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        // Constructor receives the execution action and the check function
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Event that handles changes in the command's state
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        // Checks whether the command can be executed
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        // Executes the action associated with the command
        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
