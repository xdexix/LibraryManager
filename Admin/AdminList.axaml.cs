using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using System.Data.SQLite;
using System.Linq;
using System;
using System.IO;
namespace LibraryManager
{
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
            string ratio; string baseType; string[] typeFields;
            switch(type)
            {
                case ListType.Autor:
                    ratio = "12231";  baseType = "AUTOR";
                    typeFields = new string[] {"ID", "Name", "Surname", "Country", "Birth"};
                    break;
                case ListType.Publishing:
                    ratio = "12224"; baseType = "PUBLISHING";
                    typeFields = new string[] {"ID", "Title", "Town", "Country", "Adress"};
                    break;
                case ListType.Reader:
                    ratio = "122224"; baseType = "READER";
                    typeFields = new string[] {"ID", "Name", "Surname", "Email", "Phone", "Adress"};
                    break;
                case ListType.Librarian:
                    ratio = "1332"; baseType = "LIBRARIAN";
                    typeFields = new string[] {"ID", "Name", "Surname", "POST"};
                    break;
                case ListType.Rent:
                    ratio = "111222"; baseType = "RENT";
                    typeFields = new string[] {"ID", "Reader", "Librarian", "Status", "Start_t", "End_t"};
                    break;
                case ListType.Book:
                    ratio = "1111314";  baseType = "BOOK";
                    typeFields = new string[] {"ID", "Autor", "Publishing", "Rent", "Title", "Publish", "Genre"};
                    break;
                default:
                    ratio = ""; baseType = ""; typeFields = new string[] {""};
                    break;
            }
            if (type != ListType.None)
            {
            BlockTypes.Text = "";
            foreach (string name in typeFields) BlockTypes.Text += name + "\t\t\t";
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=Data/DataBase.db;Version=3;"))
            {
            connection.Open();
            string com = (bookRent) ? "SELECT * FROM " + baseType + " WHERE Rent = 0;": "SELECT * FROM " + baseType;
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    CustomItem grid = new CustomItem();
                    string[] parts = new string[100]; int index = 0;
                    foreach (char width in ratio) grid.ColumnDefinitions.Add
                        (new ColumnDefinition { Width = new GridLength(width-'0', GridUnitType.Star) });
                    
                    #pragma warning disable CS8601
                    foreach (string typeF in typeFields) { parts[index] = reader[typeF].ToString(); index++; }
                    #pragma warning restore CS8601

                    for (int i = 0; i < parts.Length; i++)
                    {
                        TextBlock textBox = new TextBlock { Text = parts[i] };
                        Grid.SetColumn(textBox, i);
                        grid.Children.Add(textBox);
                    }

                    grid.Fill(parts);
                    ObjectList.Items.Add(grid);
                }
            }
            }
            connection.Close();
            }
            }
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
}