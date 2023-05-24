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
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QualityControl_WinUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReviewPage : Page
    {
        private string[] answers = { "������� �� �����! �� � ��������� ��������� �� ������� ��������� � ���� �����, ��� ���� ������ ���������� �������.",
                    "������� �� ���������� �������! �� ��������� �������� ������, ����� ������������ ����� �������� ������ ������.",
                    "���������� ��� �� ����������� ���������! ���� ������������� ������ ����������� ��� ������������� ��� ����� ������� ������� �����",
                    "���������� ��� �� ���������� ���� ������! �� ���������� ��������� �������� ������, ����� ������������ ��� ������ ������." ,
                    "������� �� ������! ��� ����� ������� �������, ��� ��� �������� ���� ������.",
                    "������� �� ����������� �������! �� ���� ���� ������ ������ ������!",
                    "���������� ��� �� ������������� �����! �� ����� �������� ������ � ������������� ��� ������ ������.",
                    "������� ������� �� �����! �� ����, ��� ��� �������� ��, ��� �� ������.",
                    "������� ��� �� ����� ���������! ���� ������� ��������� ������������� ��� ������������ ������.",
                    "������� �� ���������! ���� ������� ����� ���������� �������� ��� ���������� � ��������������� ������ �����.",
                    "���������� �� ��� ������������� �����! ���� ������� ���� ������������� ��� ������ ������.",
                    "������� ��� �� ����������� �������! �� ���� �����, ��� ���� ������ ������ ���.",
                    "���������� �� ��� ������������� �����! ���� ������� ��������� �������� ������, ����� ������������ ��� ������ ������.",
                    "������� ��� �� ���������! ���� ������� ��������� �������� ������, ����� ������������ ��� ������ ������.",
                    "���������� ��� �� ������ � �����! ���� ������� ���������� ���������� � �������.",
                    "���������� ��� �� �����! ���� ����� ��� ��� ����� ����� � �������� ��� ���������� � �������!",
                    "���������� ��� �� ������������� �����! �� ���������� ��������� �������� ������, ����� ������������� ��� ������ ������.",
                    "���������� ��� �� ���������! ���� ������� ��������� �������� ������, ����� ������������� ��� ������ ������.",
                    "���������� ��� �� ������������� �����! ���� ������� ���� ������������� ��� ������ ������.",
                    "���������� ��� �� ���������! �� ����, ��� ���� ������ ������������� ����� ������������.",
                    "������ ����! �� ������������ ��� �� �������� ����� � ������� ������!",
                    "������ ����! ������� �� �����. ���� � ��� ���� ����, ��� ������� ��� ������ ��� �����, ����������, �������� ��� �� ilorussia@qualitycontroldepartment.ru . ��� ����� ����� ���� ������.",
                    "���������� �� ��� ����� � ��������� ����� ��� ��������� ������!",
                    "������ ����! ������� �� ����� � �������� ����� ������. � ���������, ������� �������������� ������������ ����",
                    "������ ����! ����� ��������� � ������ �������� � ��� ������ ������������� ������ �� ������������� ����� ��������! �������, ��� ��������� ���!",
                    "������������! ���������� ��� �� ������� ������ �������� ��������������� �����!"};
        
        public ReviewPage()
        {
            this.InitializeComponent();
            DataListView.ItemsSource = answers;
        }

        private void DataListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var package = new DataPackage();
            package.SetText(e.ClickedItem as String);
            Clipboard.SetContent(package);
        }

        private void RandomBtn_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();            
            var package = new DataPackage();
            package.SetText(answers[rnd.Next(0, answers.Length - 1)]);
            Clipboard.SetContent(package);
        }
    }
}
