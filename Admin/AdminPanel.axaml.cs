using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
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
        }
        private void AdminPassword_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Text == Password)
            {
                var adminWindow = new AdminWindow();
                adminWindow.Show();
                this.Hide();
            }
            else
            {
                passwordBox.Text = "";
                passwordBox.Focus();
            }
        }
    }
}