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
    /// Загрузк данных из базы данных.
    /// </summary>
    /// <param name="id">ID загружаемого элемента.</param>
    /// <param name="connection">Путь к базе данных.</param>
    /// <param name="parameters">Дополнительные параметры.</param>
    public bool LoadFromBase(int id, SQLiteConnection connection, List<string>? parameters = null) 
    {
        string com = "SELECT * FROM AUTOR WHERE ID = @ID";
        using (connection)
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                if (parameters != null)
                {
                    foreach (string parameter in parameters)
                    {
                        string[] param = parameter.Split('=');
                        command.Parameters.AddWithValue(param[0], param[1]);
                    }
                }
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
    /// Загрузк данных из базы данных.
    /// </summary>
    /// <param name="id">ID загружаемого элемента.</param>
    /// <param name="connection">Путь к базе данных.</param>
    /// <param name="parameters">Дополнительные параметры.</param>
    public bool LoadFromBase(int id, SQLiteConnection connection, List<string>? parameters = null) 
    {
        string com = "SELECT * FROM PUBLISHING WHERE ID = @ID";
        using (connection)
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                if (parameters != null)
                {
                    foreach (string parameter in parameters)
                    {
                        string[] param = parameter.Split('=');
                        command.Parameters.AddWithValue(param[0], param[1]);
                    }
                }
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
    public bool LoadFromBase(int id, SQLiteConnection connection, List<string>? parameters = null) 
    {
        string com = "SELECT * FROM READER WHERE ID = @ID";
        using (connection)
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                if (parameters != null)
                {
                    foreach (string parameter in parameters)
                    {
                        string[] param = parameter.Split('=');
                        command.Parameters.AddWithValue(param[0], param[1]);
                    }
                }
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
    public bool LoadFromBase(int id, SQLiteConnection connection, List<string>? parameters = null) 
    { 
        string com = "SELECT * FROM LIBRARIAN WHERE ID = @ID";
        using (connection)
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                if (parameters != null)
                {
                    foreach (string parameter in parameters)
                    {
                        string[] param = parameter.Split('=');
                        command.Parameters.AddWithValue(param[0], param[1]);
                    }
                }
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
    public bool LoadFromBase(int id, SQLiteConnection connection, List<string>? parameters = null) 
    { 
        string com = "SELECT * FROM RENT WHERE ID = @ID";
        using (connection)
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                if (parameters != null)
                {
                    foreach (string parameter in parameters)
                    {
                        string[] param = parameter.Split('=');
                        command.Parameters.AddWithValue(param[0], param[1]);
                    }
                }
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
    public bool LoadFromBase(int id, SQLiteConnection connection, List<string>? parameters = null) 
    {
        string com = "SELECT * FROM BOOK WHERE ID = @ID";
        using (connection)
        { 
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(com, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                if (parameters != null)
                {
                    foreach (string parameter in parameters)
                    {
                        string[] param = parameter.Split('=');
                        command.Parameters.AddWithValue(param[0], param[1]);
                    }
                }
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

                        connection.Close(); return true;
                    }
                    else { connection.Close(); return false; }
                }
            }
        }
    }
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