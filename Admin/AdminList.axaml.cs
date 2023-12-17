using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using System.Data.SQLite;
using System.Linq;
using System;
namespace LibraryManager
{
    public enum ListType
    {
        Autor, Publishing, Book, Librarian, Rent, Reader, None
    }
    public partial class AdminList : Window
    {
        public string? SelectedItem { get; set; }
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
        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ObjectList.SelectedItem != null) 
            {
                string selectedItemString = ObjectList.SelectedItem.ToString();
                string[] parts = selectedItemString.Split(',');
                string firstPart = parts[0];
                string intValue = firstPart;
                SelectedItem = intValue;
                this.Close();
            }
        }
        public class CustomItem : Grid
        {
            public CustomItem() : base() { }
            public int ID;
            public string[]? Parts;
            public void Fill(string[] parts)
            {
                System.Int32.TryParse(parts[0], out ID);
                Parts = parts;
                for (int i = 0; i < parts.Length; i++)
                {
                    TextBlock textBox = new TextBlock { Text = parts[i] };
                    Grid.SetColumn(textBox, i);
                    Children.Add(textBox);
                }
            }
            public override string? ToString()
            {
                return string.Join(", ", Parts);
            }
        }
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
        private void FindBox_Changed(object sender, TextChangedEventArgs e)
        {   
            string? filteredText = findBox.Text;
            var filteredItems = full.Items.Where(item => item?.ToString() != null && item.ToString().ToLower().Contains(filteredText.ToLower()));
            ObjectList.Items.Clear();
            foreach (var item in filteredItems)
            {
                ObjectList.Items.Add(item);
            }
        }
        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            CreateList(type);
            full.Items.Clear();
            foreach (var item in ObjectList.Items) { full.Items.Add(item); }
        }
        ListType type;
        ItemsControl full = new ItemsControl();
        public AdminList(ListType _type, bool bookRent = false)
        { 
            InitializeComponent();
            Bitmap bitmap = new Bitmap("Images/list.png");
            this.Icon = new WindowIcon(bitmap);
            if (_type == ListType.None) Buttons.IsVisible = true;
            type = _type;
            CreateList(type, bookRent);
            foreach (var item in ObjectList.Items) { full.Items.Add(item); }
        }
    }
}