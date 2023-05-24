// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using QualityControl_WinUI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QualityControl_WinUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsViewModel ViewModel { get; }

        public SettingsPage()
        {
            this.InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<SettingsViewModel>();
        }

        private void themeMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (themeMode.SelectedIndex)
            {
                case 0:
                    ViewModel.UpdateTheme("Light");
                    break;

                case 1:
                    ViewModel.UpdateTheme("Dark");
                    break;

                case 2:
                    ViewModel.UpdateTheme("Default");
                    break;      
            }

        }
    }
}
