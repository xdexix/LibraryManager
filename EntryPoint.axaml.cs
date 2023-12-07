using Avalonia.Controls;
using Avalonia.Interactivity;
namespace LibraryManager
{
    public partial class EntryPoint : Window
    {
        public EntryPoint()
        {
            InitializeComponent();
        }
        private void AdminPanel_Click(object sender, RoutedEventArgs e)
        {
            var adminPanel = new AdminPanel();
            adminPanel.Show();
            this.Hide();
        }
        private void UserPanel_Click(object sender, RoutedEventArgs e)
        {
            var userPanel = new UserPanel();
            userPanel.Show();
            this.Hide();
        }
    }
}