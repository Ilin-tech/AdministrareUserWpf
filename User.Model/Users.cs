using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace User.Model
{
    // The Users class represents a data model for users and implements INotifyPropertyChanged to notify property changes
    public class Users : INotifyPropertyChanged
    {
        // Private fields for storing property values
        private int _id;
        private string _username;
        private string _password;
        private bool _isAdmin;

        // ID property with change notification
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(); // Notifies value change
            }
        }

        // UsernAME property with change notification
        public string UsernAME
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(); // Notifies value change
            }
        }

        // uSERpASS property with change notification
        public string uSERpASS
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(); // Notifies value change
            }
        }

        // IsAdmin property with change notification
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged(); // Notifies value change
            }
        }

        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to trigger the PropertyChanged event when a property's value changes
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
