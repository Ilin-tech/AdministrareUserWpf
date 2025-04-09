using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace User.Model
{
    // Clasa Users reprezintă un model de date pentru utilizatori și implementează INotifyPropertyChanged pentru a notifica schimbările de proprietăți
    public class Users : INotifyPropertyChanged
    {
        // Câmpuri private pentru stocarea valorilor proprietăților
        private int _id;
        private string _username;
        private string _password;
        private bool _isAdmin;

        // Proprietatea ID cu notificare la schimbare
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(); // Notifică schimbarea valorii
            }
        }

        // Proprietatea UsernAME cu notificare la schimbare
        public string UsernAME
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(); // Notifică schimbarea valorii
            }
        }

        // Proprietatea uSERpASS cu notificare la schimbare
        public string uSERpASS
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(); // Notifică schimbarea valorii
            }
        }

        // Proprietatea IsAdmin cu notificare la schimbare
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged(); // Notifică schimbarea valorii
            }
        }

        // Eveniment pentru notificarea schimbărilor de proprietăți
        public event PropertyChangedEventHandler PropertyChanged;

        // Metodă pentru a declanșa evenimentul PropertyChanged când o proprietate își schimbă valoarea
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
