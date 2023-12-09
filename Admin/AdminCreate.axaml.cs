using System.Collections.Generic;
using System.Threading;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            Bitmap bitmap = new Bitmap("Images/icon.png");
            this.Icon = new WindowIcon(bitmap);
            type = create;
            switch(create)
            {
                case Create.Autor: 
                    CreateField("Имя");
                    CreateField("Фамилия");
                    CreateField("Страна");
                    CreateField("Дата рождения");
                    break;  
                case Create.Publishing: 
                    CreateField("Название");
                    CreateField("Город");
                    CreateField("Страна");
                    CreateField("Адрес");
                    break;
                case Create.Book: 
                    CreateField("ТУТ");
                    CreateField("НИЧЕГО");
                    CreateField("НЕТ");
                    break;
                case Create.Librarian: 
                    CreateField("Имя");
                    CreateField("Фамилия");
                    CreateField("Индекс");
                    break;
                default: break;
            }
            var stackPanel = this.FindControl<StackPanel>("MyStackPanel");

            var button = new Button();
            button.Content = "Сохранить";
            button.Width = 200;
            button.Margin = new Avalonia.Thickness(0, 20, 0, 0);
            button.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
            button.HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center;

            stackPanel?.Children.Add(button);
        }
    }
}