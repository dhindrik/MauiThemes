using System.Text.Json;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MauiThemes;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        WeakReferenceMessenger.Default.Register<ThemeChangedMessage>(this, (r, m) =>
        {
            LoadTheme(m.Value);
        });

        var theme = Preferences.Get("theme", "System");

         RequestedThemeChanged += (s, a) =>
         {
            LoadTheme(theme);
         };

        LoadTheme(theme);
    }

    private void LoadTheme(string theme)
    {
        if (!MainThread.IsMainThread)
        {
            MainThread.BeginInvokeOnMainThread(() => LoadTheme(theme));
            return;
        }

        if (theme == "System")
        {
            theme = Current.PlatformAppTheme.ToString();
        }

        ResourceDictionary dictionary = theme switch
        {
            "Dark" => new Resources.Styles.Dark(),
            "Light" => new Resources.Styles.Light(),
            "Red" => new Resources.Styles.Red(),
            "Blue" => new Resources.Styles.Blue(),
            "Custom" => LoadCustomTheme(),
            _ => null
        };

        if (dictionary != null)
        {
            Resources.MergedDictionaries.Clear();

            Resources.MergedDictionaries.Add(dictionary);
        }
    }

    private ResourceDictionary LoadCustomTheme()
    {
      var json = Preferences.Get("CustomTheme", null);

        var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

        var dictionary = new ResourceDictionary();

        foreach(var item in data)
        {
            dictionary.Add(item.Key, Color.FromArgb(item.Value));
        }

        return dictionary;
    }
}

