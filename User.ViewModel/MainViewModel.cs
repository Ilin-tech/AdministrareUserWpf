using Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using User.Model;
using User.Services;

namespace User.ViewModel
{
    // ViewModel pentru gestionarea utilizatorilor
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly DataService _dataService;
        private Users _selectedUser;

        // Colecție de utilizatori pentru listare în interfață
        public ObservableCollection<Users> Users { get; set; }

        // Comenzi pentru salvare și adăugare de utilizatori
        public ICommand SaveCommand { get; }
        public ICommand AddCommand { get; }

        // Utilizatorul selectat în prezent
        public Users SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(); }
        }

        // Constructor pentru inițializarea serviciului de date și a comenzilor
        public MainViewModel()
        {
            _dataService = new DataService();
            Users = _dataService.GetAllUsers();
            SaveCommand = new RelayCommand(Save, CanSave);
            AddCommand = new RelayCommand(AddUser);
        }

        // Verifică dacă utilizatorul selectat nu este nul înainte de a permite salvarea
        private bool CanSave()
        {
            return SelectedUser != null;
        }

        // Metodă pentru salvarea modificărilor utilizatorului selectat
        private void Save()
        {
            if (SelectedUser != null)
            {
                try
                {
                    _dataService.UpdateUser(SelectedUser);
                    MessageBox.Show("User updated successfully.");
                    OnPropertyChanged(nameof(SelectedUser));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Save failed: {ex.Message}");
                }
            }
        }

        // Metodă pentru adăugarea unui utilizator nou în listă
        private void AddUser()
        {
            try
            {
                var newUser = new Users { UsernAME = "NewUser", uSERpASS = "password", IsAdmin = false };
                _dataService.AddUser(newUser);
                Users.Add(newUser);
                SelectedUser = newUser;
                MessageBox.Show("New user added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Add failed: {ex.Message}");
            }
        }

        // Implementare pentru notificarea schimbării proprietăților
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
