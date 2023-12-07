using Avalonia.Controls;
namespace LibraryManager
{
    public partial class UserWindow : Window
    {
        public UserWindow(int ID = 0)
        {
            InitializeComponent();
            HelloText.Text += ", привет ID " + ID.ToString();
        }
    }
}
