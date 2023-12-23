using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
namespace LibraryManager;
/// <summary>
/// Класс авторизации библиотекаря.
/// </summary>
public partial class UserPanel : Window
{
    /// <summary>
    /// Конструктор UserPanel.
    /// Инициализирует компоненты окна и устанавливает иконку icon.png.
    /// Устанавливает фокус на поле для ввода ID.
    /// </summary>
    public UserPanel()
    { 
        InitializeComponent();
        string imagePath = Path.Combine("Images", "icon.png");
        Bitmap bitmap = new Bitmap(imagePath);
        this.Icon = new WindowIcon(bitmap);
        IDBox.Focus();
    }
    /// <summary>
    /// Обработчик события нажатия на кнопку продолжения.
    /// Создает новый экземляр UserWindow(ID). Производит проверку корректности ID.
    /// </summary>
    private void UserID_Click(object sender, RoutedEventArgs e)
    {
        int id; 
        if (int.TryParse(IDBox.Text, out id))
        {
            var userWindow = new UserWindow(id);
            userWindow.Show();
            this.Close();
        }
        else
        {
            IDBox.Text = "";
            IDBox.Focus();
        }
    }
}