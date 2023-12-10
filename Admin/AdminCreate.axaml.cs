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
        Create type;
        private List<TextBox> textBoxes = new List<TextBox>();
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
                    this.Height = 3 * 75 + 100;
                    CreateField("ТУТ");
                    CreateField("НИЧЕГО");
                    CreateField("НЕТ");
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
    }
}