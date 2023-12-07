using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
namespace LibraryManager
{
    public partial class AdminPanel : Window
    {
        private const string Password = "admin";
        public AdminPanel()
        {
            InitializeComponent();
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