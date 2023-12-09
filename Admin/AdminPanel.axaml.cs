using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
namespace LibraryManager
{
    public partial class AdminPanel : Window
    {
        private const string Password = "admin";
        public AdminPanel()
        { 
            InitializeComponent();
            Bitmap bitmap = new Bitmap("Images/icon.png");
            this.Icon = new WindowIcon(bitmap);
            passwordBox.Focus();
        }
        private void AdminPassword_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Text == Password)
            {
                var adminWindow = new AdminWindow();
                adminWindow.Show();
                this.Close();
            }
            else
            {
                passwordBox.Text = "";
                passwordBox.Focus();
            }
        }
    }
}