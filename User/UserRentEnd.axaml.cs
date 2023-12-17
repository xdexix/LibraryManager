using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using System.Data.SQLite;
using System.Linq;
using System;
namespace LibraryManager
{
    public partial class RentList : Window
    {
        public string? SelectedItem { get; set; }
        public void CreateRemoveList()
        {
            ObjectList.Items.Clear();
            string ratio = "111222"; string[] typeFields  = new string[] {"ID", "Reader", "Librarian", "Status", "Start_t", "End_t"};
            BlockTypes.Text = "";
            foreach (string name in typeFields) BlockTypes.Text += name + "\t\t\t";
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=Data/DataBase.db;Version=3;"))
            {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM RENT", connection))
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
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ObjectList.SelectedItem != null) 
            {
                using (var connection = new SQLiteConnection("Data Source=Data/DataBase.db")) 
                {
                    connection.Open();
                    foreach (var item in ObjectList.SelectedItems ?? full.Items)
                    {   
                        string? selectedItemString = item?.ToString();
                        string[]? parts = selectedItemString?.Split(',');
                        string? firstPart = parts?[0];
                        string? intValue = firstPart;

                        string sql1 = "DELETE FROM RENT WHERE ID = @ID";
                        SQLiteCommand command1 = new SQLiteCommand(sql1, connection);
                        command1.Parameters.AddWithValue("@ID", intValue);
                        command1.ExecuteNonQuery();

                        string sql2 = "UPDATE BOOK SET RENT = 0 WHERE RENT = @RentID";
                        SQLiteCommand command2 = new SQLiteCommand(sql2, connection);
                        command2.Parameters.AddWithValue("@RentID", intValue);
                        command2.ExecuteNonQuery();
                    }
                    connection.Close();
                }

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
                if (Parts != null) return string.Join(", ", Parts);
                else return "";
            }
        }
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
        ItemsControl full = new ItemsControl();
        public RentList()
        { 
            InitializeComponent();
            Bitmap bitmap = new Bitmap("Images/delete.png");
            this.Icon = new WindowIcon(bitmap);
            CreateRemoveList();
            foreach (var item in ObjectList.Items) { full.Items.Add(item); }
        }
    }
}