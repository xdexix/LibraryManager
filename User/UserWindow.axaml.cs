using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
namespace LibraryManager;
/// <summary>
/// Класс окна интерфейса для пользователя.
/// </summary>
public partial class UserWindow : Window
{
    /// ID библиотекаря.
    private int ID; 
    /// <summary>
    /// Конструктор UserWindow.
    /// Инициализирует компоненты окна и устанавливает иконку icon.png.
    /// Устанавливает название окна в соответствии с ID вошедшего.
    /// </summary>
    /// <param name="id">ID библиотекаря.</param>
    public UserWindow(int id)
    { 
        InitializeComponent();
        string imagePath = Path.Combine("Images", "icon.png");
        Bitmap bitmap = new Bitmap(imagePath);
        this.Icon = new WindowIcon(bitmap);
        this.Title="ID" + id + " LibraryManager v1.0";
        ID = id;
    }
    /// <summary>
    /// Обработчик события нажатия на кнопки создания.
    /// Создает новый экземляр AdminCreate нужного типа.
    /// </summary>
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
        AdminCreate create = new AdminCreate(type, rent, ID);
        create.Show();
    }
    /// <summary>
    /// Обработчик события нажатия на кнопки просмотра.
    /// Создает новый экземляр AdminList нужного типа.
    /// </summary>
    private void Button_List(object sender, RoutedEventArgs e)
    {
        Button button = (Button)sender;
        ListType type;
        switch(button.Content)
        {
            case "Читатель":    type = ListType.Reader;       break;
            case "Аренда":      type = ListType.Rent;         break;
            case "Книга":       type = ListType.Book;         break;
            default:            type = ListType.Autor;        break;
        }
        AdminList list = new AdminList(type);
        list.Show();
    }
    /// <summary>
    /// Обработчик события нажатия на кнопку удаления аренды.
    /// Создает новый экземляр RentList нужного типа.
    /// </summary>
    private void Remove_List(object sender, RoutedEventArgs e)
    {
        RentList remove = new RentList();
        remove.Show();
    }
}