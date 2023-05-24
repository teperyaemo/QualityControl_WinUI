using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QualityControl_WinUI
{
    public class SettingsViewModel : ObservableObject
    {
        private string? _topTitle;
        private readonly IThemeSelectorService _themeSelectorService;

        public string? TopTitle
        {
            get => _topTitle;
            set => SetProperty(ref _topTitle, value); 
        }

        public ICommand SetThemeCommand { get; }

        public SettingsViewModel(IThemeSelectorService themeSelectorService )
        {
            SetThemeCommand = new RelayCommand<string>((theme) => UpdateTheme(theme));
            _themeSelectorService = themeSelectorService;
        }

        public void UpdateTheme(string themeName)
        {
            
            if (Enum.TryParse(themeName, out ElementTheme theme) is true)
            {
                _themeSelectorService.SetTheme(theme);
            }
        }
    }
}
