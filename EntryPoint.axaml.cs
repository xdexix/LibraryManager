using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
namespace LibraryManager
{ 
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
            this.Close();
        }
        private void UserPanel_Click(object sender, RoutedEventArgs e)
        {
            var userPanel = new UserPanel();
            userPanel.Show();
            this.Close();
        }
    }
}