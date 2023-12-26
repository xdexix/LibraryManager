using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using LibraryManager;
using Avalonia.Controls;
using System.CodeDom;
/// <summary>
/// Базовый класс типов элементов базы данных.
/// </summary>
public class BDTypes
{
    /// ID элемента
    protected int ID { get; set; }
    /// <summary>
    /// Создание таблицы для отображения содержимого элементов ListBox.
    /// </summary>
    /// <param name="ratio">Соотношение строк таблицы для отображения.</param>
    protected CustomItem CreateGrid(string ratio)
    {
        CustomItem grid = new CustomItem();
        foreach (char width in ratio) grid.ColumnDefinitions.Add
            (new ColumnDefinition { Width = new GridLength(width-'0', GridUnitType.Star) });
        return grid;
    }
    /// <summary>
    /// Проверка введенных данных для создания записей в базе данных.
    /// </summary>
    public bool IsValid(object[]? parameters)
    {
        if (parameters == null) { return false; }

        for (int i = 0; i < parameters?.Length; i++)
        {
            if (parameters[i] is int)
            {
                if ((int)parameters[i] < 0) { return false; }
            }
            else if (parameters[i] is string)
            {
                if (string.IsNullOrEmpty(parameters[i] as string)) { return false; }
            }
        }
        return true;
    }
}
/// <summary>
/// Класс объектов Автор. Наследование от базового типа DBTypes.
/// </summary>
public class Autor : BDTypes
{
    private string? Name { get; set; }
    private string? Surname { get; set; }
    private string? Country { get; set; }
    private string? Birth { get; set; }
    /// <summary>
    /// Пустой автор.
    /// </summary>
    public Autor() {Name = ""; Surname = ""; Country = ""; Birth= ""; }
    /// <summary>
    /// Сохранение Автора в базу данных.
    /// </summary>
    /// <param name="connection">Путь к базе данных.</param>
    /// <param name="_name">Имя автора.</param>
    /// <param name="_surname">Фамилия автора.</param>
    /// <param name="_country">Страна автора.</param>
    /// <param name="_birth">Год рождения автора.</param>
    public Autor(SQLiteConnection connection, string? _name, string? _surname, string? _country, string? _birth)
    {
        this.Name = _name;
        this.Surname = _surname;
        this.Country = _country;
        this.Birth = _birth;

        if (!IsValid(new object[] {Name ?? "", Surname ?? "", Country ?? "", Birth ?? "" })) { return; }

        string sql = "INSERT INTO AUTOR (Name, Surname, Country, Birth) VALUES (@Name, @Surname, @Country, @Birth)";
        SQLiteCommand command = new SQLiteCommand(sql, connection);

        command.Parameters.AddWithValue("@Name", Name);
        command.Parameters.AddWithValue("@Surname", Surname);
        command.Parameters.AddWithValue("@Country", Country);
        command.Parameters.AddWithValue("@Birth", Birth);

        command.ExecuteNonQuery();
    }
    /// <summary>
    /// Загрузка данных из базы данных.
    /// </summary>
    /// <param name="id">ID загружаемого элемента.</param>
    /// <param name="connection">Путь к базе данных.</param>
    public bool LoadFromBase(int id, SQLiteConnection connection) 
    {
        string com = "SELECT * FROM AUTOR WHERE ID = @ID";
        using (connection)
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        this.ID = reader.GetInt32(0);
                        this.Name = reader.GetString(1);
                        this.Surname = reader.GetString(2);
                        this.Country = reader.GetString(3);
                        this.Birth = reader.GetString(4);

                        connection.Close(); return true;
                    }
                    else { connection.Close(); return false; }
                }
            }
        }
    }
    /// <summary>
    /// Преобразование в GustomItem : AvaloniaGrid для отображения в ListBox.
    /// </summary>
    public CustomItem ToListBoxItem()
    {
        CustomItem grid = CreateGrid("12231");
        string[] parts = { this.ID.ToString(), this.Name ?? "Н/Д", this.Surname ?? "", this.Country ?? "", this.Birth ?? ""};

        for (int i = 0; i < parts.Length; i++)
        {
            TextBlock textBox = new TextBlock { Text = parts[i] };
            Grid.SetColumn(textBox, i);
            grid.Children.Add(textBox);
        }

        grid.Fill(parts);
        return grid;
    }
}
/// <summary>
/// Класс объектов Издательство. Наследование от базового типа DBTypes.
/// </summary>
public class Publishing : BDTypes
{
    private string? Title { get; set; }
    private string? Town { get; set; }
    private string? Country { get; set; }
    private string? Adress { get; set; }
    /// <summary>
    /// Пустое издательство.
    /// </summary>
    public Publishing() { Title = ""; Town= ""; Country = ""; Adress = ""; }
    /// <summary>
    /// Сохранение Издательства в базу данных.
    /// </summary>
    /// <param name="connection">Путь к базе данных.</param>
    /// <param name="_title">Название издательства.</param>
    /// <param name="_town">Город издательства.</param>
    /// <param name="_country">Страна издательства.</param>
    /// <param name="_adress">Адрес издательства.</param>
    public Publishing(SQLiteConnection connection, string? _title, string? _town, string? _country, string? _adress)
    {
        if (!IsValid(new object[] {_title ?? "", _town ?? "", _country ?? "", _adress ?? ""})) { return; }

        string sql = "INSERT INTO PUBLISHING (Title, Town, Country, Adress) VALUES (@Title, @Town, @Country, @Adress)";
        SQLiteCommand command = new SQLiteCommand(sql, connection);

        command.Parameters.AddWithValue("@Title", _title);
        command.Parameters.AddWithValue("@Town", _town);
        command.Parameters.AddWithValue("@Country", _country);
        command.Parameters.AddWithValue("@Adress", _adress);

        command.ExecuteNonQuery();
    }
    /// <summary>
    /// Загрузка данных из базы данных.
    /// </summary>
    /// <param name="id">ID загружаемого элемента.</param>
    /// <param name="connection">Путь к базе данных.</param>
    public bool LoadFromBase(int id, SQLiteConnection connection) 
    {
        string com = "SELECT * FROM PUBLISHING WHERE ID = @ID";
        using (connection)
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        this.ID = reader.GetInt32(0);
                        this.Title = reader.GetString(1);
                        this.Town = reader.GetString(2);
                        this.Country = reader.GetString(3);
                        this.Adress = reader.GetString(4);

                        connection.Close(); return true;
                    }
                    else { connection.Close(); return false; }
                }
            }
        }
    }
    /// <summary>
    /// Преобразование в GustomItem : AvaloniaGrid для отображения в ListBox.
    /// </summary>
    public CustomItem ToListBoxItem()
    {
        CustomItem grid = CreateGrid("12224");
        string[] parts = { this.ID.ToString(), this.Title ?? "Н/Д", this.Country ?? "Н/Д", this.Adress ?? "Н/Д" };

        for (int i = 0; i < parts.Length; i++)
        {
            TextBlock textBox = new TextBlock { Text = parts[i] };
            Grid.SetColumn(textBox, i);
            grid.Children.Add(textBox);
        }

        grid.Fill(parts);
        return grid;
    }
}
/// <summary>
/// Класс объектов Читатель. Наследование от базового типа DBTypes.
/// </summary>
public class Reader : BDTypes
{
    private string? Name { get; set; }
    private string? Surname { get; set; }
    private string? Email { get; set; }
    private string? Phone { get; set; }
    private string? Adress { get; set; }
    /// <summary>
    /// Пустой читатель.
    /// </summary>
    public Reader() { Name = ""; Surname= ""; Email = ""; Phone = ""; Adress = ""; }
    /// <summary>
    /// Сохранение Читателя в базу данных.
    /// </summary>
    /// <param name="connection">Путь к базе данных.</param>
    /// <param name="_name">Имя читателя.</param>
    /// <param name="_surname">Фамилия читателя.</param>
    /// <param name="_email">Email читателя.</param>
    /// <param name="_phone">Телефон читателя.</param>
    /// <param name="_adress">Адрес читателя.</param>
    public Reader(SQLiteConnection connection, string? _name, string? _surname, string? _email, string? _phone, string? _adress)
    {
        if (!IsValid(new object[] {_name ?? "", _surname ?? "", _email?? "", _phone ?? "", _adress ?? ""})) { return; }

        string sql = "INSERT INTO READER (Name, Surname, Email, Phone, Adress) VALUES (@Name, @Surname, @Email, @Phone, @Adress)";
        SQLiteCommand command = new SQLiteCommand(sql, connection);

        command.Parameters.AddWithValue("@Name", _name);
        command.Parameters.AddWithValue("@Surname", _surname);
        command.Parameters.AddWithValue("@Email", _email);
        command.Parameters.AddWithValue("@Phone", _phone);
        command.Parameters.AddWithValue("@Adress", _adress);

        command.ExecuteNonQuery();
    }
    /// <summary>
    /// Загрузка данных из базы данных.
    /// </summary>
    /// <param name="id">ID загружаемого элемента.</param>
    /// <param name="connection">Путь к базе данных.</param>
    public bool LoadFromBase(int id, SQLiteConnection connection) 
    {
        string com = "SELECT * FROM READER WHERE ID = @ID";
        using (connection)
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        this.ID = reader.GetInt32(0);
                        this.Name = reader.GetString(1);
                        this.Surname = reader.GetString(2);
                        this.Email = reader.GetString(3);
                        this.Phone = reader.GetString(4);
                        this.Adress = reader.GetString(5);

                        connection.Close(); return true;
                    }
                    else { connection.Close(); return false; }
                }
            }
        }
    }
    /// <summary>
    /// Преобразование в GustomItem : AvaloniaGrid для отображения в ListBox.
    /// </summary>
    public CustomItem ToListBoxItem()
    { 
        CustomItem grid = CreateGrid("122224");
        string[] parts = { this.ID.ToString(), this.Name ?? "Н/Д", this.Surname ?? "Н/Д", this.Email ?? "Н/Д", this.Phone ?? "Н/Д", this.Adress ?? "Н/Д"};

        for (int i = 0; i < parts.Length; i++)
        {
            TextBlock textBox = new TextBlock { Text = parts[i] };
            Grid.SetColumn(textBox, i);
            grid.Children.Add(textBox);
        }

        grid.Fill(parts);
        return grid;
    }
}
/// <summary>
/// Класс объектов Библиотекарь. Наследование от базового типа DBTypes.
/// </summary>
public class Librarian : BDTypes
{
    private string? Name { get; set; }
    private string? Surname { get; set; }
    private string? POST { get; set; }
    /// <summary>
    /// Пустой библиотекарь.
    /// </summary>
    public Librarian() { Name = ""; Surname= ""; POST = ""; }
    /// <summary>
    /// Сохранение Библиотекаря в базу данных.
    /// </summary>
    /// <param name="connection">Путь к базе данных.</param>
    /// <param name="_name">Имя библиотекаря.</param>
    /// <param name="_surname">Фамилия библиотекаря.</param>
    /// <param name="_post">Индекс библиотекаря.</param>
    public Librarian(SQLiteConnection connection, string? _name, string? _surname, string? _post)
    {
        if (!IsValid(new object[] {_name ?? "", _surname ?? "", _post ?? ""})) { return; }

        string sql = "INSERT INTO LIBRARIAN (Name, Surname, POST) VALUES (@Name, @Surname, @POST)";
        SQLiteCommand command = new SQLiteCommand(sql, connection);

        command.Parameters.AddWithValue("@Name", _name);
        command.Parameters.AddWithValue("@Surname", _surname);
        command.Parameters.AddWithValue("@POST", _post);

        command.ExecuteNonQuery();
    }
    /// <summary>
    /// Загрузка данных из базы данных.
    /// </summary>
    /// <param name="id">ID загружаемого элемента.</param>
    /// <param name="connection">Путь к базе данных.</param>
    public bool LoadFromBase(int id, SQLiteConnection connection) 
    { 
        string com = "SELECT * FROM LIBRARIAN WHERE ID = @ID";
        using (connection)
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        this.ID = reader.GetInt32(0);
                        this.Name = reader.GetString(1);
                        this.Surname = reader.GetString(2);
                        this.POST = reader.GetString(3);

                        connection.Close(); return true;
                    }
                    else { connection.Close(); return false; }
                }
            }
        }
    }
    /// <summary>
    /// Преобразование в GustomItem : AvaloniaGrid для отображения в ListBox.
    /// </summary>
    public CustomItem ToListBoxItem()
    {
        CustomItem grid = CreateGrid("1332");
        string[] parts = { this.ID.ToString(), this.Name ?? "Н/Д", this.Surname ?? "Н/Д", this.POST ?? "Н/Д" };

        for (int i = 0; i < parts.Length; i++)
        {
            TextBlock textBox = new TextBlock { Text = parts[i] };
            Grid.SetColumn(textBox, i);
            grid.Children.Add(textBox);
        }

        grid.Fill(parts);
        return grid;
    }
}
/// <summary>
/// Класс объектов Аренда. Наследование от базового типа DBTypes.
/// </summary>
public class Rent : BDTypes
{
    private int Reader { get; set; }
    private int Librarian { get; set; }
    private int Status { get; set; }
    private string? Start_t { get; set; }
    private string? End_t { get; set; }
    /// <summary>
    /// Пустая аренда.
    /// </summary>
    public Rent() { Reader = 0; Librarian = 0; Status = 0; Start_t = ""; End_t = ""; }
    /// <summary>
    /// Сохранение записи аренды в базу данных.
    /// </summary>
    /// <param name="connection">Путь к базе данных.</param>
    /// <param name="_reader">ID читателя.</param>
    /// <param name="_librarian">ID библиотекаря.</param>
    /// <param name="_status">Статус аренды.</param>
    /// <param name="_start_t">Время начала аренды.</param>
    /// <param name="_end_t">Время окончания аренды.</param>
    public Rent(SQLiteConnection connection, int _reader, int _librarian, int _status, string? _start_t, string? _end_t)
    {
        if (!IsValid(new object[] {_reader, _librarian, _status, _start_t ?? "", _end_t ?? ""})) { return; }

        string sql = "INSERT INTO RENT (Reader, Librarian, Status, Start_t, End_t) VALUES (@Reader, @Librarian, @Status, @Start_t, @End_t)";
        SQLiteCommand command = new SQLiteCommand(sql, connection);

        command.Parameters.AddWithValue("@Reader", _reader);
        command.Parameters.AddWithValue("@Librarian", _librarian);
        command.Parameters.AddWithValue("@Status", _status);
        command.Parameters.AddWithValue("@Start_t", _start_t);
        command.Parameters.AddWithValue("@End_t", _end_t);

        command.ExecuteNonQuery();
    }
    /// <summary>
    /// Загрузка данных из базы данных.
    /// </summary>
    /// <param name="id">ID загружаемого элемента.</param>
    /// <param name="connection">Путь к базе данных.</param>
    public bool LoadFromBase(int id, SQLiteConnection connection) 
    { 
        string com = "SELECT * FROM RENT WHERE ID = @ID";
        using (connection)
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        this.ID = reader.GetInt32(0);
                        this.Reader = reader.GetInt32(1);
                        this.Librarian = reader.GetInt32(2);
                        this.Status = reader.GetInt32(3);
                        this.Start_t = reader.GetString(4);
                        this.End_t = reader.GetString(5);

                        connection.Close(); return true;
                    }
                    else { connection.Close(); return false; }
                }
            }
        }
    }
    /// <summary>
    /// Преобразование в GustomItem : AvaloniaGrid для отображения в ListBox.
    /// </summary>
    public CustomItem ToListBoxItem()
    {
        CustomItem grid = CreateGrid("111222");
        string[] parts = { this.ID.ToString(), this.Reader.ToString(), this.Librarian.ToString(), this.Status.ToString(),
            this.Start_t ?? "Н/Д", this.End_t ?? "Н/Д" };

        for (int i = 0; i < parts.Length; i++)
        {
            TextBlock textBox = new TextBlock { Text = parts[i] };
            Grid.SetColumn(textBox, i);
            grid.Children.Add(textBox);
        }

        grid.Fill(parts);
        return grid;
    }
}
/// <summary>
/// Класс объектов Книга. Наследование от базового типа DBTypes.
/// </summary>
public class Book : BDTypes
{
    private int Autor { get; set; }
    private int Publishing { get; set; }
    private int Rent { get; set; }
    private string? Title { get; set; }
    private string? Publish { get; set; }
    private string? Genre { get; set; } 
    /// <summary>
    /// Если ID = 0, возвращает true.
    /// </summary>
    public bool Is0ID() { if (this.ID == 0) return false; else return true; }
    /// <summary>
    /// Пустая книга.
    /// </summary>
    public Book() { Autor = 0; Publishing = 0; Rent = 0; Title = ""; Publish = ""; Genre = ""; }
    /// <summary>
    /// Сохранение книги в базу данных.
    /// </summary>
    /// <param name="connection">Путь к базе данных.</param>
    /// <param name="_autor">ID автора.</param>
    /// <param name="_publishing">ID издательства.</param>
    /// <param name="_rent">ID аренды.</param>
    /// <param name="_title">Название книги.</param>
    /// <param name="_publish">Дата публикации.</param>
    /// <param name="_genre">Жанр книги.</param>
    public Book(SQLiteConnection connection, int _autor, int _publishing, int _rent, string? _title, string? _publish, string? _genre)
    {
        if (!IsValid(new object[] {_autor, _publishing, _rent, _title ?? "", _publish ?? "", _genre ?? ""})) { return; }

        string sql = "INSERT INTO BOOK (Autor, Publishing, Rent, Title, Publish, Genre) VALUES (@Autor, @Publishing, @Rent, @Title, @Publish, @Genre)";
        SQLiteCommand command = new SQLiteCommand(sql, connection);

        command.Parameters.AddWithValue("@Autor", _autor);
        command.Parameters.AddWithValue("@Publishing", _publishing);
        command.Parameters.AddWithValue("@Rent", _rent);
        command.Parameters.AddWithValue("@Title", _title);
        command.Parameters.AddWithValue("@Publish", _publish);
        command.Parameters.AddWithValue("@Genre", _genre);

        command.ExecuteNonQuery();
    }
    /// <summary>
    /// Загрузка данных из базы данных.
    /// </summary>
    /// <param name="id">ID загружаемого элемента.</param>
    /// <param name="connection">Путь к базе данных.</param>
    /// <param name="NotRent">true для вывода неарендованных книг.</param>
    public bool LoadFromBase(int id, SQLiteConnection connection, bool NotRent = false) 
    {
        string com = "SELECT * FROM BOOK WHERE ID = @ID";
        using (connection)
        { 
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
                command.Parameters.AddWithValue("@ID", id);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        this.ID = reader.GetInt32(0);
                        this.Autor = reader.GetInt32(1);
                        this.Publishing = reader.GetInt32(2);
                        this.Rent = reader.GetInt32(3);
                        this.Title = reader.GetString(4);
                        this.Publish = reader.GetString(5);
                        this.Genre = reader.GetString(6);

                        if (NotRent) { if (this.Rent != 0) this.ID = 0; }

                        connection.Close();
                        
                        return true;
                    }
                    else { connection.Close(); return false; }
                }
            }
        }
    }
    /// <summary>
    /// Преобразование в GustomItem : AvaloniaGrid для отображения в ListBox.
    /// </summary>
    public CustomItem ToListBoxItem()
    {
        CustomItem grid = CreateGrid("1111314");
        string[] parts = { this.ID.ToString(), this.Autor.ToString(), this.Publishing.ToString(), this.Rent.ToString(),
            this.Title ?? "Н/Д", this.Publish ?? "Н/Д", this.Genre ?? "Н/Д" };

        for (int i = 0; i < parts.Length; i++)
        {
            TextBlock textBox = new TextBlock { Text = parts[i] };
            Grid.SetColumn(textBox, i);
            grid.Children.Add(textBox);
        }

        grid.Fill(parts);
        return grid;
    }
}