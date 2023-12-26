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
    /// <param name="id">ID библиотекаря.</param>
    public AdminCreate(Create create, int id = 0)
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
                CreateField(ListType.Book, true);
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
                    Autor? autor = new Autor(connection, textBoxes[0].Text, textBoxes[1].Text, textBoxes[2].Text, textBoxes[3].Text);
                    if (autor.IsValid(new object[] {textBoxes[0].Text ?? "", textBoxes[1].Text ?? "", textBoxes[2].Text ?? "", textBoxes[3].Text ?? ""})) this.Close();
                    break;
                case Create.Publishing:
                    Publishing? publishing = new Publishing(connection, textBoxes[0].Text, textBoxes[1].Text, textBoxes[2].Text, textBoxes[3].Text);
                    if (publishing.IsValid(new object[] {textBoxes[0].Text ?? "", textBoxes[1].Text ?? "", textBoxes[2].Text ?? "", textBoxes[3].Text ?? ""})) this.Close();
                    break;
                case Create.Librarian:
                    Librarian? librarian = new Librarian(connection, textBoxes[0].Text, textBoxes[1].Text, textBoxes[2].Text);
                    if (librarian.IsValid(new object[] {textBoxes[0].Text ?? "", textBoxes[1].Text ?? "", textBoxes[2].Text ?? ""})) this.Close();
                    break;
                case Create.Book:
                    int parsedValue1;
                    int parsedValue2;
                    int.TryParse(buttons[0].Content?.ToString(), out parsedValue1);
                    int.TryParse(buttons[1].Content?.ToString(), out parsedValue2);
                    Book? book = new Book(connection, parsedValue1, parsedValue2, 0, textBoxes[0].Text, textBoxes[1].Text, textBoxes[2].Text);
                    if (book.IsValid(new object[] {parsedValue1, parsedValue2, 0, textBoxes[0].Text ?? "", textBoxes[1].Text ?? "", textBoxes[2].Text ?? ""})) this.Close();
                    break;
                case Create.Reader:
                    Reader? reader = new Reader(connection, textBoxes[0].Text, textBoxes[1].Text, textBoxes[2].Text, textBoxes[3].Text, textBoxes[4].Text);
                    if (reader.IsValid(new object[] {textBoxes[0].Text ?? "", textBoxes[1].Text ?? "", textBoxes[2].Text ?? "", textBoxes[3].Text ?? "", textBoxes[4].Text ?? ""})) this.Close();
                    break;
                case Create.Rent:
                    SaveRent(connection);
                    break;
            }
            connection.Close();
        }
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
        string Start_t = DateTime.Now.ToString();
        string End_t = DateTime.Now.AddMonths(2).ToString();

        Rent? rent = new Rent(connection, parsedValue1, ID, 1, Start_t, End_t);
        if (rent.IsValid(new object[] {connection, parsedValue1, ID, 1, Start_t ?? "", End_t ?? "" })) this.Close();

        string sql = "SELECT ID FROM RENT WHERE Reader = @Reader AND Librarian = @Librarian AND Status = @Status AND Start_t = @Start_t AND End_t = @End_t; SELECT last_insert_rowid()";
        SQLiteCommand command = new SQLiteCommand(sql, connection);

        command.Parameters.AddWithValue("@Reader", parsedValue1);
        command.Parameters.AddWithValue("@Librarian", ID);
        command.Parameters.AddWithValue("@Status", 1);
        command.Parameters.AddWithValue("@Start_t", Start_t);
        command.Parameters.AddWithValue("@End_t", End_t);

        int rentId = (int)(long)command.ExecuteScalar();

        string updateSql = "UPDATE BOOK SET Rent = @Rent WHERE ID = @ID";
        SQLiteCommand updateCommand = new SQLiteCommand(updateSql, connection);

        updateCommand.Parameters.AddWithValue("@Rent", rentId);
        updateCommand.Parameters.AddWithValue("@ID", parsedValue2);

        updateCommand.ExecuteNonQuery();
        
        this.Close();
    }
}