using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
namespace LibraryManager;
/// <summary>
/// Класс окна входа в приложение.
/// </summary>
public partial class EntryPoint : Window
{
    /// <summary>
    /// Конструктор EntryPoint.
    /// Инициализирует компоненты окна и устанавливает иконку icon.png.
    /// </summary>
    public EntryPoint() 
    {             
        InitializeComponent();
        string imagePath = Path.Combine("Images", "icon.png");
        Bitmap bitmap = new Bitmap(imagePath);
        this.Icon = new WindowIcon(bitmap);
    }
    /// <summary>
    /// Обработчик события нажатия на кнопку AdminPanel.
    /// Вызывается при нажатии на кнопку "Администратор".
    /// Создает новый экземляр AdminPanel и закрывает текущее окно.
    /// </summary>
    private void AdminPanel_Click(object sender, RoutedEventArgs e)
    {
        var adminPanel = new AdminPanel();
        adminPanel.Show();
        this.Close();
    }
    /// <summary>
    /// Обработчик события нажатия на кнопку UserPanel.
    /// Вызывается при нажатии на кнопку "Пользователь".
    /// Создает новый экземляр UserPanel и закрывает текущее окно.
    /// </summary>
    private void UserPanel_Click(object sender, RoutedEventArgs e)
    {
        var userPanel = new UserPanel();
        userPanel.Show();
        this.Close();
    }
}