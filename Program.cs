using Avalonia;
using System;
namespace LibraryManager;
/// <summary>
/// Определение класса точки входа.
/// </summary>
class Program
{
    /// <summary>
    /// Точка входа MAIN в приложение.
    /// </summary>
    /// <param name="args">Аргументы командной строки.</param>
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
    /// <summary>
    /// Создает новый экземпляр AppBuilder и настраивает его.
    /// </summary>
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>() .UsePlatformDetect() .WithInterFont() .LogToTrace();
}
