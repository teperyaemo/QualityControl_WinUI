﻿// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT; // required to support Window.As<ICompositionSupportsSystemBackdrop>()
using QualityControl_WinUI.Helpers;
using Microsoft.UI.Composition.SystemBackdrops;
using Windows.Storage;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QualityControl_WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private static Window startupWindow;

        // Get the initial window created for this app
        // On UWP, this is simply Window.Current
        // On Desktop, multiple Windows may be created, and the StartupWindow may have already
        // been closed.
        public static Window StartupWindow
        {
            get
            {
                return startupWindow;
            }
        }
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            Ioc.Default.ConfigureServices(ConfigureServices());
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Window = new MainWindow();

            startupWindow = WindowHelper.CreateWindow();
            startupWindow.ExtendsContentIntoTitleBar = true;

            if (AppWindowTitleBar.IsCustomizationSupported()) //Run only on Windows 11
            {
                Window.SizeChanged += SizeChanged; //Register handler for setting draggable rects
            }
            Window.Activate();
        }

        private void SizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            //Update the title bar draggable region. We need to indent from the left both for the nav back button and to avoid the system menu
            Windows.Graphics.RectInt32[] rects = new Windows.Graphics.RectInt32[] { new Windows.Graphics.RectInt32(48, 0, (int)args.Size.Width - 48, 48) };
            
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IThemeSelectorService, ThemeSelectiorService>();
            services.AddSingleton<SettingsViewModel>();
            return services.BuildServiceProvider();
        }

        public static Window? Window { get; private set; }
        //private Window m_window;
    }
}
