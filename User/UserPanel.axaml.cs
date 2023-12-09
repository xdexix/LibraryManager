using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
namespace LibraryManager
{
    public partial class UserPanel : Window
    {
        public UserPanel()
        { 
            InitializeComponent();
            Bitmap bitmap = new Bitmap("Images/icon.png");
            this.Icon = new WindowIcon(bitmap);
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