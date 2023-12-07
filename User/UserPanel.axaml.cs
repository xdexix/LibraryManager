using Avalonia.Controls;
using Avalonia.Interactivity;
namespace LibraryManager
{
    public partial class UserPanel : Window
    {
        public UserPanel()
        {
            InitializeComponent();
        }
        private void UserID_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(IDBox.Text, out id))
            {
                var adminWindow = new UserWindow(id);
                adminWindow.Show();
                this.Hide();
            }
            else
            {
                IDBox.Text = "";
                IDBox.Focus();
            }
        }
    }
}