using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using User.UI;

namespace User.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            /* Fara on startUp
                 try
                {
                    base.OnStartup(e);
                    new LoginWindow().Show();
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Resource error: {ex.Message}");
                    throw;
                }
             */
        }
    }

}
