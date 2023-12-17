using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
namespace LibraryManager
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        { 
            InitializeComponent();
            Bitmap bitmap = new Bitmap("Images/icon.png");
            this.Icon = new WindowIcon(bitmap);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            #pragma warning disable CS8602
            string? content = button.Content.ToString();
            #pragma warning restore CS8602
            Window adminWindow;
            switch (content)
            {
                case "Автор":
                    adminWindow = new AdminCreate(Create.Autor);
                    adminWindow.Show();
                    break;
                case "Издательство":
                    adminWindow = new AdminCreate(Create.Publishing);
                    adminWindow.Show();
                    break;
                case "Книга":
                    adminWindow = new AdminCreate(Create.Book);
                    adminWindow.Show();
                    break;
                case "Библиотекарь":
                    adminWindow = new AdminCreate(Create.Librarian);
                    adminWindow.Show();
                    break;
                case "Все записи":
                    adminWindow = new AdminList(ListType.None);
                    adminWindow.Show();
                    break;
                default: break;
            }
        }
    }
}
