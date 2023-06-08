// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QualityControl_WinUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            switch (MainWindow.UserID)
            {
                case 1:
                    PolinaPR.IsActive = true;
                break;

                case 2:
                    MinislamPR.IsActive = true;
                break;

                case 3:
                    DianaPR.IsActive = true;
                break;
            }
        }

        private void MinislamBtn_Click(object sender, RoutedEventArgs e)
        {
            MinislamPR.IsActive = true;
            PolinaPR.IsActive = false;
            DianaPR.IsActive = false;
            MainWindow.UserID = 2;
        }

        private void PolinaBtn_Click(object sender, RoutedEventArgs e)
        {
            MinislamPR.IsActive = false;
            PolinaPR.IsActive = true;
            DianaPR.IsActive = false;
            MainWindow.UserID = 1;
        }

        private void DianaBtn_Click(object sender, RoutedEventArgs e)
        {
            MinislamPR.IsActive = false;
            PolinaPR.IsActive = false;
            DianaPR.IsActive = true;
            MainWindow.UserID = 3;
        }
    }
}
