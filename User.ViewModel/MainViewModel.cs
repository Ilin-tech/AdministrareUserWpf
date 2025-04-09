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
    // ViewModel for managing users
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly DataService _dataService;
        private Users _selectedUser;

        // Collection of users for listing in the UI
        public ObservableCollection<Users> Users { get; set; }

        // Commands for saving and adding users
        public ICommand SaveCommand { get; }
        public ICommand AddCommand { get; }

        // Currently selected user
        public Users SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(); }
        }

        // Constructor to initialize the data service and commands
        public MainViewModel()
        {
            _dataService = new DataService();
            Users = _dataService.GetAllUsers();
            SaveCommand = new RelayCommand(Save, CanSave);
            AddCommand = new RelayCommand(AddUser);
        }

        // Checks if the selected user is not null before allowing save
        private bool CanSave()
        {
            return SelectedUser != null;
        }

        // Method for saving changes to the selected user
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

        // Method for adding a new user to the list
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

        // Implementation for notifying property changes
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
