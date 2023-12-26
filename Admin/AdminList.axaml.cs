using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using System.Data.SQLite;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace LibraryManager;
/// <summary>
/// Тип выводимого списка ListBox.
/// </summary>
public enum ListType
{
    Autor, Publishing, Book, Librarian, Rent, Reader, None
}
/// <summary>
/// Класс окна просмотра записей.
/// </summary>
public partial class AdminList : Window
{
    /// Выбранный элемент ListBox.
    public string? SelectedItem { get; set; }
    /// <summary>
    /// Создание ListBox для выбора элемента.
    /// </summary>
    /// <param name="type">Тип выводимого листа.</param>
    /// <param name="bookRent">true, если не требуется вывод арендованных книг.</param>
    public void CreateList(ListType type, bool bookRent = false)
    {
        ObjectList.Items.Clear();
        int id = 1; string[] typeFields= System.Array.Empty<string>();
        switch(type)
        {
            case ListType.Autor:
                typeFields = new string[] {"ID", "Name", "Surname", "Country", "Birth"};
                while (true) {
                    Autor autor = new Autor();
                    if (!autor.LoadFromBase(id, new SQLiteConnection("Data Source=Data/DataBase.db;Version=3;"))) { break; }
                    ObjectList.Items.Add(autor.ToListBoxItem()); id++;
                } break;
            case ListType.Publishing:
                typeFields = new string[] {"ID", "Title", "Town", "Country", "Adress"};
                while (true) {
                    Publishing publishing = new Publishing();
                    if (!publishing.LoadFromBase(id, new SQLiteConnection("Data Source=Data/DataBase.db;Version=3;"))) { break; }
                    ObjectList.Items.Add(publishing.ToListBoxItem()); id++;
                } break;
            case ListType.Reader:
                typeFields = new string[] {"ID", "Name", "Surname", "Email", "Phone", "Adress"};
                while (true) {
                    Reader reader = new Reader();
                    if (!reader.LoadFromBase(id, new SQLiteConnection("Data Source=Data/DataBase.db;Version=3;"))) { break; }
                    ObjectList.Items.Add(reader.ToListBoxItem()); id++;
                } break;
            case ListType.Librarian:
                typeFields = new string[] {"ID", "Name", "Surname", "POST"};
                while (true) {
                    Librarian librarian = new Librarian();
                    if (!librarian.LoadFromBase(id, new SQLiteConnection("Data Source=Data/DataBase.db;Version=3;"))) { break; }
                    ObjectList.Items.Add(librarian.ToListBoxItem()); id++;
                } break;
            case ListType.Rent:
                typeFields = new string[] {"ID", "Reader", "Librarian", "Status", "Start_t", "End_t"};
                while (true) {
                    Rent rent = new Rent();
                    if (!rent.LoadFromBase(id, new SQLiteConnection("Data Source=Data/DataBase.db;Version=3;"))) { break; }
                    ObjectList.Items.Add(rent.ToListBoxItem()); id++; 
                }  break;
            case ListType.Book:
                typeFields = new string[] {"ID", "Autor", "Publishing", "Rent", "Title", "Publish", "Genre"};
                while (true) {
                    Book book = new Book();
                    if (!book.LoadFromBase(id, new SQLiteConnection("Data Source=Data/DataBase.db;Version=3;"), bookRent)) { break; }
                    if (book.Is0ID()) ObjectList.Items.Add(book.ToListBoxItem()); id++;
                } break;
            default: break;
        }
        if (type != ListType.None) BlockTypes.Text = "";
        foreach (string name in typeFields) BlockTypes.Text += name + "\t\t\t";
    }
    /// <summary>
    /// Обработчик события нажатия на кнопку выбора.
    /// При вызове сохраняет выбранный элемент как SelectedItem,
    /// после закрывает текущее окно.
    /// </summary>
    private void SelectButton_Click(object sender, RoutedEventArgs e)
    {
        if (ObjectList.SelectedItem != null) 
        {
            string? selectedItemString = ObjectList.SelectedItem.ToString();
            string[]? parts = selectedItemString?.Split(',');
            string? firstPart = parts?[0];
            string? intValue = firstPart;
            SelectedItem = intValue;
            this.Close();
        }
    }
    /// <summary>
    /// Обработчик события нажатия на кнопку выбора типа листа.
    /// При нажатии сменяет тип выводимого ListBox внутри текущего окна на другой тип,
    /// а так же обновляет полный массив значений.
    /// </summary>
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Button button = (Button)sender;
        switch(button.Content)
        {
            case "Автор":           type = ListType.Autor;      break;
            case "Издательство":    type = ListType.Publishing; break;
            case "Читатель":        type = ListType.Reader;     break;
            case "Библиотекарь":    type = ListType.Librarian;  break;
            case "Аренда":          type = ListType.Rent;       break;
            case "Книга":           type = ListType.Book;       break;
            default:                type = ListType.None;       break;
        }
        CreateList(type);

        full.Items.Clear();
        foreach (var item in ObjectList.Items) { full.Items.Add(item); }
    }
    /// <summary>
    /// Обработчик события изменения строки поиска.
    /// При вызове выводит ListBox содержащий только содержимое FindBox.
    /// </summary>
    private void FindBox_Changed(object sender, TextChangedEventArgs e)
    {   
        string filteredText = (findBox.Text == null) ? "" : findBox.Text;
        var filteredItems = full.Items.Where(item => (item?.ToString() ?? "").ToLower().Contains(filteredText.ToLower()));
        ObjectList.Items.Clear();
        foreach (var item in filteredItems)
        {
            ObjectList.Items.Add(item);
        }
    }
    /// <summary>
    /// Обработчик события нажатия на кнопку перезагрузки.
    /// Вызывает CreateList() нужного типа и обновляет данные полного списка.
    /// </summary>
    private void Reload_Click(object sender, RoutedEventArgs e)
    {
        CreateList(type);
        full.Items.Clear();
        foreach (var item in ObjectList.Items) { full.Items.Add(item); }
    }
    /// Тип текущего выводимого листа в окне.
    ListType type;
    /// Все значения нужного типа.
    ItemsControl full = new ItemsControl();
    /// <summary>
    /// Конструктор AdminList.
    /// Инициализирует компоненты окна и устанавливает иконку list.png.
    /// Инициализиует стартовый лист и сохраняет его.
    /// </summary>
    /// <param name="_type">Тип выводимого листа.</param>
    /// <param name="bookRent">true, если требуется вывод неарендованных книг.</param>
    public AdminList(ListType _type, bool bookRent = false)
    { 
        InitializeComponent();
        string imagePath = Path.Combine("Images", "list.png");
        Bitmap bitmap = new Bitmap(imagePath);
        this.Icon = new WindowIcon(bitmap);
        if (_type == ListType.None) Buttons.IsVisible = true;
        type = _type;
        CreateList(type, bookRent);
        foreach (var item in ObjectList.Items) { full.Items.Add(item); }
    }
}