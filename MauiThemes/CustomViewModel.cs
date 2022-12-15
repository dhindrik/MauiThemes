using System;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace MauiThemes
{
    [ObservableObject]
    public partial class CustomViewModel
    {
        public CustomViewModel()
        {
            var json = Preferences.Get("CustomTheme", null);

            if(json != null)
            {
                var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                BackgroundColor = data["BackgroundColor"];
                TextColor = data["TextColor"];
                InputBackgroundColor = data["InputBackgroundColor"];
            }
        }

        [ObservableProperty]
        private string backgroundColor;

        [ObservableProperty]
        private string textColor;

        [ObservableProperty]
        private string inputBackgroundColor;

        [RelayCommand]
        private void Save()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("BackgroundColor", BackgroundColor);
            dictionary.Add("TextColor", TextColor);
            dictionary.Add("InputBackgroundColor", InputBackgroundColor);

            var json = JsonSerializer.Serialize(dictionary);

            Preferences.Set("CustomTheme", json);

            Preferences.Set("theme", "Custom");

            WeakReferenceMessenger.Default.Send(new ThemeAddedMessage("Custom"));
        }
    }
}

