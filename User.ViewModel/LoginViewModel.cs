using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using User.Services; // Pentru DataService

namespace User.ViewModel
{
    // Clasa LoginViewModel implementează logica de autentificare și notificare a schimbărilor în MVVM
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private readonly DataService _dataService;

        // Proprietatea pentru numele de utilizator
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        // Proprietatea pentru parola
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        // Proprietatea pentru afișarea mesajului de eroare
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        // Comanda pentru acțiunea de autentificare
        public ICommand LoginCommand { get; }

        // Eveniment care semnalează autentificarea reușită
        public event EventHandler LoginSuccessful;

        // Constructor pentru inițializarea serviciului de date și a comenzii de login
        public LoginViewModel()
        {
            _dataService = new DataService();
            // Inițializare comandă de login cu apel la metoda Login
            LoginCommand = new RelayCommand(() => Login());
        }

        // Metoda de autentificare
        private void Login()
        {
            // Validează utilizatorul utilizând serviciul de date
            var user = _dataService.ValidateUser(Username, Password);

            // Dacă utilizatorul este valid, declanșează evenimentul de succes
            if (user != null)
            {
                LoginSuccessful?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                // Afișează mesaj de eroare dacă autentificarea a eșuat
                ErrorMessage = "Login failed. Please try again.";
            }
        }

        // Eveniment pentru notificarea schimbării unei proprietăți
        public event PropertyChangedEventHandler PropertyChanged;

        // Metodă pentru a declanșa evenimentul PropertyChanged
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Clasa RelayCommand pentru implementarea interfeței ICommand
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        // Constructorul primește acțiunea de execuție și funcția de verificare
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Eveniment care gestionează schimbările în starea comenzii
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        // Verifică dacă comanda poate fi executată
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        // Execută acțiunea asociată comenzii
        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
