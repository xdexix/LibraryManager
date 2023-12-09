using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
namespace LibraryManager
{
    public partial class UserPanel : Window
    {
        public UserPanel()
        { 
            InitializeComponent();
            Bitmap bitmap = new Bitmap("Images/icon.png");
            this.Icon = new WindowIcon(bitmap);
            IDBox.Focus();
        }
        private void UserID_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(IDBox.Text, out id))
            {
                var adminWindow = new UserWindow(id);
                adminWindow.Show();
                this.Close();
            }
            else
            {
                IDBox.Text = "";
                IDBox.Focus();
            }
        }
    }
}