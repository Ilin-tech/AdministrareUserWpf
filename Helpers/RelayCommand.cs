using System;
using System.Windows.Input;

namespace Helpers
{
    // The RelayCommand class implements the ICommand interface to provide support for commands in MVVM
    public class RelayCommand : ICommand
    {
        // Delegate for the command execution action
        private readonly Action _execute;

        // Delegate for checking the condition for command execution
        private readonly Func<bool> _canExecute;

        // The constructor receives two delegates: one for execution and one for validation
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            // Checks if the execution action is not null
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Event for notifying when the command's execution status changes
        public event EventHandler CanExecuteChanged;

        // Method that determines whether the command can be executed
        public bool CanExecute(object parameter)
        {
            // If the _canExecute delegate is null, the command can be executed
            return _canExecute == null || _canExecute();
        }

        // Method that executes the action associated with the command
        public void Execute(object parameter)
        {
            _execute();
        }

        // Method to trigger the CanExecuteChanged event, indicating that the command state has changed
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
