using System;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
namespace LibraryManager
{
/*    
    public partial class BaseWindow : Window
    {
        public BaseWindow()
        {
            InitializeComponent();
            Bitmap bitmap = new Bitmap("Images/icon.png");
            this.Icon = new WindowIcon(bitmap);
        }
    }
*/
    public partial class EntryPoint : Window
    {
        public EntryPoint()
        { 
            InitializeComponent();
            Bitmap bitmap = new Bitmap("Images/icon.png");
            this.Icon = new WindowIcon(bitmap);
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