# LibraryManager
## Назначение

Library Manager - это программное обеспечение, предназначенное для управления и организации коллекции книг библиотеки. 
Он предоставляет функциональность для добавления новых книг, удаления книг, поиска книг, а также для осуществления 
административных функций.

## Важно

Перед установкой убедитесь в том, что в вашей системе установлен .NET7 и SQLite.

Установка этих пакетов для **Linux**:

  **.NET 7** для **Ubuntu 22.04** (https://learn.microsoft.com/ru-ru/dotnet/core/install/linux): 
```bash
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

sudo dpkg -i packages-microsoft-prod.deb

sudo apt-get update

sudo apt-get install -y apt-transport-https

sudo apt-get update

sudo apt-get install -y dotnet-sdk-7.0
```
  **SQLite**: 
```bash
sudo apt-get install sqlite3
```

## Установка

1. Скачайте пакет удобным вам способом.

```bash
git clone https://github.com/xdexix/LibraryManager.git
```

2. Произведите сборку в зависимости от целевой платформы.

    **Windows**: Откройте файл .sln в VisualStudio.
   
    **Linux и MscOS**: Войдите в директорию проекта и выполните запуск.
```bash
cd LibraryManager

dotnet run 
```
3. Если это необходимо, замените файл базы данных Data/DataBase.db.
```bash
cp [ваш файл] LibraryManager/Data/DataBase.db 
```

## Полезные ссылки
.NET Windows: https://learn.microsoft.com/ru-ru/dotnet/core/install/windows

.NET Linux: https://learn.microsoft.com/ru-ru/dotnet/core/install/linux

.NET MasOS: https://learn.microsoft.com/ru-ru/dotnet/core/install/macos

Диспетчер пакетов NuGet: https://learn.microsoft.com/ru-ru/nuget/

Браузер для базы данных SQLite: https://sqlitebrowser.org/ 
