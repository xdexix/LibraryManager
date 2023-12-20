using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
namespace LibraryManager
{
    /// <summary>
    /// Класс окна интерфейса для администратора.
    /// </summary>
    public partial class AdminWindow : Window
    {
        /// <summary>
        /// Конструктор AdminWindow.
        /// Инициализирует компоненты окна и устанавливает иконку icon.png.
        /// </summary>
        public AdminWindow()
        { 
            InitializeComponent();
            Bitmap bitmap = new Bitmap("Images/icon.png");
            this.Icon = new WindowIcon(bitmap);
        }
        /// <summary>
        /// Обработчик события нажатия на кнопки действия.
        /// Создание Автора, Издательства, книги или библиотекаря, создание AdminCreate() соответствующего типа.
        /// Вывод AdminList(ListType.None), с выбором типа внутри AdminList.
        /// </summary>
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
