using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
namespace LibraryManager;
/// <summary>
/// Основной класс приложения.
/// Инициализирует компоненты окна.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Инициализирует приложение.
    /// </summary>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    /// <summary>
    /// Вызывается после завершения инициализации фреймворка.
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new EntryPoint();
        }
        base.OnFrameworkInitializationCompleted();
    }
}