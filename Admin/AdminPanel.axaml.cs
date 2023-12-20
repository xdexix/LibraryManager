using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
namespace LibraryManager
{
    /// <summary>
    /// Класс авторизации администратора.
    /// </summary>
    public partial class AdminPanel : Window
    {
        /// Пароль администратора.
        private const string Password = "admin";
        /// <summary>
        /// Конструктор AdminPanel.
        /// Инициализирует компоненты окна и устанавливает иконку icon.png.
        /// Устанавливает фокус на поле для ввода ID.
        /// </summary>
        public AdminPanel()
        { 
            InitializeComponent();
            Bitmap bitmap = new Bitmap("Images/icon.png");
            this.Icon = new WindowIcon(bitmap);
            passwordBox.Focus();
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку продолжения.
        /// Создает новый экземляр AdminWindow(). Производит проверку корректности пароля.
        /// </summary>
        private void AdminPassword_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Text == Password)
            {
                var adminWindow = new AdminWindow();
                adminWindow.Show();
                this.Close();
            }
            else
            {
                passwordBox.Text = "";
                passwordBox.Focus();
            }
        }
    }
}