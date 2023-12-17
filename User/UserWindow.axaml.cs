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
            this.Title="ID" + id + " LibraryManager v1.0";
        }
        private void Button_Create(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Create type; bool rent = false;
            switch(button.Content)
            {
                case "Читатель":    type = Create.Reader;                   break;
                case "Аренда":      type = Create.Rent;     rent = true;    break;  
                default:            type = Create.Autor;                    break;
            }
            AdminCreate create = new AdminCreate(type, rent);
            create.Show();
        }
        private void Button_List(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ListType type;
            switch(button.Content)
            {
                case "Читатель":    type = ListType.Reader;       break;
                case "Аренда":      type = ListType.Rent;         break;
                default:            type = ListType.Autor;        break;
            }
            AdminList list = new AdminList(type);
            list.Show();
        }
    }
}
