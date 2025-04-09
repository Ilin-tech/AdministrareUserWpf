using System;
using System.Windows.Input;

namespace Helpers
{
    // Clasa RelayCommand implementează interfața ICommand pentru a oferi suport pentru comenzi în MVVM
    public class RelayCommand : ICommand
    {
        // Delegat pentru acțiunea de executare a comenzii
        private readonly Action _execute;

        // Delegat pentru verificarea condiției de executare a comenzii
        private readonly Func<bool> _canExecute;

        // Constructorul primește două delegate: unul pentru execuție și unul pentru validare
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            // Verifică dacă acțiunea de execuție nu este nulă
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Eveniment pentru notificarea schimbării stării de execuție a comenzii
        public event EventHandler CanExecuteChanged;

        // Metodă care determină dacă comanda poate fi executată
        public bool CanExecute(object parameter)
        {
            // Dacă delegatul _canExecute este nul, comanda poate fi executată
            return _canExecute == null || _canExecute();
        }

        // Metodă care execută acțiunea asociată comenzii
        public void Execute(object parameter)
        {
            _execute();
        }

        // Metodă pentru a declanșa evenimentul CanExecuteChanged, indicând că starea comenzii s-a schimbat
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
