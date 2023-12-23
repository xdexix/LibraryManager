using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using System.Data.SQLite;
using System;
using System.IO;
namespace LibraryManager;
/// <summary>
/// Тип создаваемого элемента.
/// </summary>
public enum Create
{
    Autor, Publishing, Book, Librarian, Reader, Rent
}
/// <summary>
/// Класс окна создания записей.
/// </summary>
public partial class AdminCreate : Window
{    
    /// ID библиотекаря.
    private int ID;
    /// <summary>
    /// Создание TextBox поля для ввода значений создаваемого элемента.
    /// </summary>
    /// <param name="nameField">Строка названия</param>
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
    /// <summary>
    /// Создание кнопки для вывода листа для выбора значений создаваемого элемента.
    /// </summary>
    /// <param name="nameField">Строка названия</param>
    /// <param name="rent">true, если не требуется вывод арендованных книг.</param>
    public void CreateField(ListType NameField, bool rent = false)
    {
        var stackPanel = this.FindControl<StackPanel>("MyStackPanel");

        string nameField;
        switch(NameField)
        {
            case ListType.Autor: nameField = "Автор"; break;
            case ListType.Publishing: nameField = "Издательство"; break;
            case ListType.Reader: nameField = "Читатель"; break;
            case ListType.Book: nameField = "Книга"; break;
            case ListType.Librarian: nameField = "Библиотекарь"; break;
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
        button.Click += (sender, e) => ButtonList_Click(button, e, NameField, rent);

        stackPanel?.Children.Add(textBlock);
        stackPanel?.Children.Add(button);

        buttons.Add(button);
    }
    /// <summary>
    /// Обработчик события нажатия на кнопку выбора внутри листа.
    /// Сохраняет выбранный элемент для дальнейшего сохранения.
    /// </summary>
    /// <param name="type">Тип выводимого листа AdminList.</param>
    /// <param name="bookRent">true, если не требуется вывод арендованных книг.</param>
    private void ButtonList_Click(Button sender, RoutedEventArgs e, ListType type, bool bookRent = false)
    {
        AdminList adminList = new AdminList(type, bookRent);
        adminList.Closed += (s, args) => { sender.Content = adminList.SelectedItem; };
        adminList.Show();
    }
    /// Тип текущего создаваемого листа в окне.
    Create type;
    /// Лист содержащий значения вводимых в TextBox значений CreateField().
    private List<TextBox> textBoxes = new List<TextBox>();
    /// Лист содержащий элементы выбранные в ListBox CreateField().
    private List<Button> buttons = new List<Button>();
    /// <summary>
    /// Конструктор AdminCreate.
    /// Инициализирует компоненты окна и устанавливает иконку plus.png.
    /// Создание полей для ввода значений элементов для сохранения.
    /// </summary>
    /// <param name="create">Тип создаваемого элемента.</param>
    /// <param name="rent">true, если не требуется вывод арендованных книг.</param>
    /// <param name="id">ID библиотекаря.</param>
    public AdminCreate(Create create, bool rent = false, int id = 0)
    { 
        InitializeComponent();
        string imagePath = Path.Combine("Images", "plus.png");
        Bitmap bitmap = new Bitmap(imagePath);
        this.Icon = new WindowIcon(bitmap);
        type = create; ID = id;
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
            case Create.Reader: 
                this.Title = "Новый читатель";
                this.Height = 5 * 75 + 100;
                CreateField("Имя");
                CreateField("Фамилия");
                CreateField("Email");
                CreateField("Телефон");
                CreateField("Адрес");
                break;
            case Create.Rent: 
                this.Title = "Новая аренда";
                this.Height = 2 * 75 + 100;
                CreateField(ListType.Reader);
                CreateField(ListType.Book, rent);
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
    /// <summary>
    /// Обработчик события нажатия на кнопку сохранения.
    /// При нажатии вызывает необходимую функцию записи в базу данных.
    /// </summary>
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
                case Create.Reader:
                    SaveReader(connection);
                    break;
                case Create.Rent:
                    SaveRent(connection);
                    break;
            }
            connection.Close();
        }
    }
    /// <summary>
    /// Сохранение автора в базу данных.
    /// </summary>
    /// <param name="connection">Путь к базе данных SQLite.</param>
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
    /// <summary>
    /// Сохранение издательства в базу данных.
    /// </summary>
    /// <param name="connection">Путь к базе данных SQLite.</param>
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
    /// <summary>
    /// Сохранение библиотекаря в базу данных.
    /// </summary>
    /// <param name="connection">Путь к базе данных SQLite.</param>
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
    /// <summary>
    /// Сохранение книги в базу данных.
    /// </summary>
    /// <param name="connection">Путь к базе данных SQLite.</param>
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
    /// <summary>
    /// Сохранение читателя в базу данных.
    /// </summary>
    /// <param name="connection">Путь к базе данных SQLite.</param>
    private void SaveReader(SQLiteConnection connection)
    {
        var reader = new 
        { 
            Name = textBoxes[0].Text, 
            Surname = textBoxes[1].Text, 
            Email = textBoxes[2].Text,
            Phone = textBoxes[3].Text,
            Adress = textBoxes[4].Text
        };

        if (string.IsNullOrEmpty(reader.Name) || string.IsNullOrEmpty(reader.Surname) || string.IsNullOrEmpty(reader.Email)
            || string.IsNullOrEmpty(reader.Phone) || string.IsNullOrEmpty(reader.Adress))
        {
            return;
        }

        string sql = "INSERT INTO READER (Name, Surname, Email, Phone, Adress) VALUES (@Name, @Surname, @Email, @Phone, @Adress)";
        SQLiteCommand command = new SQLiteCommand(sql, connection);

        command.Parameters.AddWithValue("@Name", reader.Name);
        command.Parameters.AddWithValue("@Surname", reader.Surname);
        command.Parameters.AddWithValue("@Email", reader.Email);
        command.Parameters.AddWithValue("@Phone", reader.Phone);
        command.Parameters.AddWithValue("@Adress", reader.Adress);

        command.ExecuteNonQuery();
        this.Close();
    }
    /// <summary>
    /// Сохранение аренды в базу данных.
    /// </summary>
    /// <param name="connection">Путь к базе данных SQLite.</param>
    private void SaveRent(SQLiteConnection connection)
    {
        int parsedValue1; int parsedValue2;
        int.TryParse(buttons[0].Content?.ToString(), out parsedValue1);
        int.TryParse(buttons[1].Content?.ToString(), out parsedValue2);
        var rent = new 
        { 
            Reader = parsedValue1, 
            Librarian = ID, 
            Status = 1, 
            Start_t = DateTime.Now.ToString(),
            End_t = DateTime.Now.AddMonths(2).ToString()
        };

        string sql = "INSERT INTO RENT (Reader, Librarian, Status, Start_t, End_t) VALUES (@Reader, @Librarian, @Status, @Start_t, @End_t)";
        SQLiteCommand command = new SQLiteCommand(sql, connection);

        command.Parameters.AddWithValue("@Reader", rent.Reader);
        command.Parameters.AddWithValue("@Librarian", rent.Librarian);
        command.Parameters.AddWithValue("@Status", rent.Status);
        command.Parameters.AddWithValue("@Start_t", rent.Start_t);
        command.Parameters.AddWithValue("@End_t", rent.End_t);

        command.ExecuteNonQuery();

        string new_sql = "SELECT ID FROM RENT WHERE Reader = @Reader AND Librarian = @Librarian AND Status = @Status AND Start_t = @Start_t AND End_t = @End_t; SELECT last_insert_rowid()";
        SQLiteCommand new_command = new SQLiteCommand(new_sql, connection);

        new_command.Parameters.AddWithValue("@Reader", rent.Reader);
        new_command.Parameters.AddWithValue("@Librarian", rent.Librarian);
        new_command.Parameters.AddWithValue("@Status", rent.Status);
        new_command.Parameters.AddWithValue("@Start_t", rent.Start_t);
        new_command.Parameters.AddWithValue("@End_t", rent.End_t);

        int rentId = (int)(long)new_command.ExecuteScalar();

        string updateSql = "UPDATE BOOK SET Rent = @Rent WHERE ID = @ID";
        SQLiteCommand updateCommand = new SQLiteCommand(updateSql, connection);

        updateCommand.Parameters.AddWithValue("@Rent", rentId);
        updateCommand.Parameters.AddWithValue("@ID", parsedValue2);

        updateCommand.ExecuteNonQuery();
        
        this.Close();
    }
}