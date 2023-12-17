using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using SqlTools;
using System.Data.SQLite;
using System.Linq;

namespace LibraryManager
{
    public enum Create
    {
        Autor, Publishing, Book, Librarian
    }

    public partial class AdminCreate : Window
    {    
        public void CreateField(string nameField)
        {
            var stackPanel = this.FindControl<StackPanel>("MyStackPanel");

            var textBlock = new TextBlock();
            textBlock.Text = nameField;
            textBlock.Margin = new Avalonia.Thickness(0, 20, 0, 0);
            textBlock.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;

            var textBox = new TextBox();
            textBox.Width = 200;
            textBox.Margin = new Avalonia.Thickness(0, 10, 0, 0);
            textBox.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;

            stackPanel?.Children.Add(textBlock);
            stackPanel?.Children.Add(textBox);

            textBoxes.Add(textBox);
        }
        public void CreateField(ListType NameField)
        {
            var stackPanel = this.FindControl<StackPanel>("MyStackPanel");

            string nameField;
            switch(NameField)
            {
                case ListType.Autor: nameField = "Автор"; break;
                case ListType.Publishing: nameField = "Издательство"; break;
                case ListType.Reader: nameField = "Читатель"; break;
                default: nameField = " "; break;
            }
            var textBlock = new TextBlock();
            textBlock.Text = nameField;
            textBlock.Margin = new Avalonia.Thickness(0, 20, 0, 0);
            textBlock.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;

            var button = new Button();
            button.Content = "Выбрать";
            button.Width = 200;
            button.Margin = new Avalonia.Thickness(0, 10, 0, 0);
            button.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
            button.Click += (sender, e) => ButtonList_Click(button, e, NameField);

            stackPanel?.Children.Add(textBlock);
            stackPanel?.Children.Add(button);

            buttons.Add(button);
        }
        private void ButtonList_Click(Button sender, RoutedEventArgs e, ListType type)
        {
            AdminList adminList = new AdminList(type);
            adminList.Closed += (s, args) => { sender.Content = adminList.SelectedItem; };
            adminList.Show();
        }
        Create type;
        private List<TextBox> textBoxes = new List<TextBox>();
        private List<Button> buttons = new List<Button>();
        public AdminCreate(Create create)
        { 
            InitializeComponent();
            Bitmap bitmap = new Bitmap("Images/plus.png");
            this.Icon = new WindowIcon(bitmap);
            type = create;
            switch(create)
            {
                case Create.Autor: 
                    this.Title = "Новый автор";
                    this.Height = 4 * 75 + 100;
                    CreateField("Имя");
                    CreateField("Фамилия");
                    CreateField("Страна");
                    CreateField("Дата рождения");
                    break;  
                case Create.Publishing: 
                    this.Title = "Новое издательство";
                    this.Height = 4 * 75 + 100;
                    CreateField("Название");
                    CreateField("Город");
                    CreateField("Страна");
                    CreateField("Адрес");
                    break;
                case Create.Book: 
                    this.Title = "Новая книга";
                    this.Height = 5 * 75 + 100;
                    CreateField(ListType.Autor);
                    CreateField(ListType.Publishing);
                    CreateField("Название");
                    CreateField("Год");
                    CreateField("Жанр");
                    break;
                case Create.Librarian: 
                    this.Title = "Новый библиотекарь";
                    this.Height = 3 * 75 + 100;
                    CreateField("Имя");
                    CreateField("Фамилия");
                    CreateField("Индекс");
                    break;
                default: break;
            }
            var stackPanel = this.FindControl<StackPanel>("MyStackPanel");

            var button = new Button();
            button.Content = "Сохранить";
            button.Click += Button_Click;
            button.Width = 200;
            button.Margin = new Avalonia.Thickness(0, 20, 0, 0);
            button.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
            button.HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center;

            stackPanel?.Children.Add(button);
        }
        private void Button_Click(object? sender, RoutedEventArgs e)
        {
            using (var connection = new SQLiteConnection("Data Source=Data/DataBase.db")) 
            {
                connection.Open();
                switch (type)
                {
                    case Create.Autor:
                        SaveAutor(connection);
                        break;
                    case Create.Publishing:
                        SavePublishing(connection);
                        break;
                    case Create.Librarian:
                        SaveLibrarian(connection);
                        break;
                    case Create.Book:
                        SaveBook(connection);
                        break;
                }
                connection.Close();
            }
        }
        private void SaveAutor(SQLiteConnection connection)
        {
            var autor = new 
            { 
                Name = textBoxes[0].Text, 
                Surname = textBoxes[1].Text, 
                Country = textBoxes[2].Text, 
                Birth = textBoxes[3].Text 
            };

            if (string.IsNullOrEmpty(autor.Name) || string.IsNullOrEmpty(autor.Surname) || string.IsNullOrEmpty(autor.Country) || string.IsNullOrEmpty(autor.Birth))
            {
                return;
            }

            string sql = "INSERT INTO AUTOR (Name, Surname, Country, Birth) VALUES (@Name, @Surname, @Country, @Birth)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);

            command.Parameters.AddWithValue("@Name", autor.Name);
            command.Parameters.AddWithValue("@Surname", autor.Surname);
            command.Parameters.AddWithValue("@Country", autor.Country);
            command.Parameters.AddWithValue("@Birth", autor.Birth);

            command.ExecuteNonQuery();
            this.Close();
        }
        private void SavePublishing(SQLiteConnection connection)
        {
            var publishing = new 
            { 
                Title = textBoxes[0].Text, 
                Town = textBoxes[1].Text, 
                Country = textBoxes[2].Text, 
                Adress = textBoxes[3].Text 
            };

            if (string.IsNullOrEmpty(publishing.Title) || string.IsNullOrEmpty(publishing.Town) || string.IsNullOrEmpty(publishing.Country) || string.IsNullOrEmpty(publishing.Adress))
            {
                return;
            }

            string sql = "INSERT INTO PUBLISHING (Title, Town, Country, Adress) VALUES (@Title, @Town, @Country, @Adress)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);

            command.Parameters.AddWithValue("@Title", publishing.Title);
            command.Parameters.AddWithValue("@Town", publishing.Town);
            command.Parameters.AddWithValue("@Country", publishing.Country);
            command.Parameters.AddWithValue("@Adress", publishing.Adress);

            command.ExecuteNonQuery();
            this.Close();
        }
        private void SaveLibrarian(SQLiteConnection connection)
        {
            var librarian = new 
            { 
                Name = textBoxes[0].Text, 
                Surname = textBoxes[1].Text, 
                POST = textBoxes[2].Text 
            };

            if (string.IsNullOrEmpty(librarian.Name) || string.IsNullOrEmpty(librarian.Surname) || string.IsNullOrEmpty(librarian.POST))
            {
                return;
            }

            string sql = "INSERT INTO LIBRARIAN (Name, Surname, POST) VALUES (@Name, @Surname, @POST)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);

            command.Parameters.AddWithValue("@Name", librarian.Name);
            command.Parameters.AddWithValue("@Surname", librarian.Surname);
            command.Parameters.AddWithValue("@POST", librarian.POST);

            command.ExecuteNonQuery();
            this.Close();
        }
        private void SaveBook(SQLiteConnection connection)
        {
            int parsedValue1;
            int parsedValue2;
            int.TryParse(buttons[0].Content?.ToString(), out parsedValue1);
            int.TryParse(buttons[1].Content?.ToString(), out parsedValue2);
            var book = new 
            { 
                Autor = parsedValue1, 
                Publishing = parsedValue2, 
                Rent = 0, 
                Title = textBoxes[0].Text,
                Publish = textBoxes[1].Text, 
                Genre = textBoxes[2].Text
            };

            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Publish) || string.IsNullOrEmpty(book.Genre))
            {
                return;
            }

            string sql = "INSERT INTO BOOK (Autor, Publishing, Rent, Title, Publish, Genre) VALUES (@Autor, @Publishing, @Rent, @Title, @Publish, @Genre)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);

            command.Parameters.AddWithValue("@Autor", book.Autor);
            command.Parameters.AddWithValue("@Publishing", book.Publishing);
            command.Parameters.AddWithValue("@Rent", book.Rent);
            command.Parameters.AddWithValue("@Title", book.Title);
            command.Parameters.AddWithValue("@Publish", book.Publish);
            command.Parameters.AddWithValue("@Genre", book.Genre);

            command.ExecuteNonQuery();
            this.Close();
        }
    }
}