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
        private void AdminWindow_Click(object sender, RoutedEventArgs e)
        {
            var adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Hide();
        }
        private void UserPanel_Click(object sender, RoutedEventArgs e)
        {
            var userPanel = new UserPanel();
            userPanel.Show();
            this.Hide();
        }
        private void UserWindow_Click(object sender, RoutedEventArgs e)
        {
            var userWindow = new UserWindow();
            userWindow.Show();
            this.Hide();
        }
    }
}