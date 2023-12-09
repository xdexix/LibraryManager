using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
namespace LibraryManager
{
    public partial class UserWindow : Window
    {
        public UserWindow(int id)
        { 
            InitializeComponent();
            Bitmap bitmap = new Bitmap("Images/icon.png");
            this.Icon = new WindowIcon(bitmap);
            HelloText.Text += ", " + id;
        }
    }
}
