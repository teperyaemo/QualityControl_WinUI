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
using System.Diagnostics;
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
        private string[] answers = { "Спасибо за отзыв! Мы с гордостью выступаем за высокие стандарты и рады знать, что наши услуги пользуются успехом.",
                    "Спасибо за выраженное доверие! Мы прилагаем максимум усилий, чтобы предоставить нашим Клиентам лучшие услуги.",
                    "Благодарим Вас за проявленную поддержку! Ваши положительные отзывы вдохновляют нас предоставлять еще более высокий уровень услуг",
                    "Благодарим Вас за отмеченный нами подход! Мы продолжаем прилагать максимум усилий, чтобы предоставить Вам лучшие услуги." ,
                    "Спасибо за оценку! Нам очень приятно слышать, что Вам нравятся наши услуги.",
                    "Спасибо за проявленное доверие! Мы рады быть частью Вашего успеха!",
                    "Благодарим Вас за положительные слова! Мы будем работать дальше и предоставлять Вам лучшие услуги.",
                    "Большое спасибо за отзыв! Мы рады, что Вам нравится то, что мы делаем.",
                    "Спасибо Вам за слова поощрения! Наша команда старается предоставлять Вам качественные услуги.",
                    "Спасибо за поддержку! Наша команда будет продолжать работать над улучшением и предоставлением лучших услуг.",
                    "Благодарим за Ваш положительный отзыв! Наша команда рада предоставлять Вам лучшие услуги.",
                    "Спасибо Вам за проявленное доверие! Мы рады знать, что наши услуги радуют Вас.",
                    "Благодарим за Ваш положительный отзыв! Наша команда прилагает максимум усилий, чтобы предоставить Вам лучшие услуги.",
                    "Спасибо Вам за поддержку! Наша команда прилагает максимум усилий, чтобы предоставить Вам лучшие услуги.",
                    "Благодарим Вас за оценку и отзыв! Наша команда продолжает стремиться к лучшему.",
                    "Благодарим Вас за отзыв! Этот отзыв для нас очень важен и помогает нам стремиться к лучшему!",
                    "Благодарим Вас за положительный отзыв! Мы продолжаем прилагать максимум усилий, чтобы предоставлять Вам лучшие услуги.",
                    "Благодарим Вас за поддержку! Наша команда прилагает максимум усилий, чтобы предоставлять Вам лучшие услуги.",
                    "Благодарим Вас за положительный отзыв! Наша команда рада предоставлять Вам лучшие услуги.",
                    "Благодарим Вас за поддержку! Мы рады, что наши услуги соответствуют Вашим потребностям.",
                    "Добрый день! Мы признательны Вам за приятный отзыв и высокую оценку!",
                    "Добрый день! Спасибо за отзыв. Если у Вас есть идеи, как сделать наш сервис еще лучше, пожалуйста, напишите нам на ilorussia@qualitycontroldepartment.ru . Нам очень важно Ваше мнение.",
                    "Благодарим за ваш выбор и уделенное время для написания отзыва!",
                    "Добрый день! Спасибо за отзыв о качестве нашей работы. С уважением, команда Международного Юридического Бюро",
                    "Добрый день! Будем стараться и дальше вызывать у вас только положительные эмоции от использования наших сервисов! Спасибо, что выбираете нас!",
                    "Здравствуйте! Благодарим Вас за высокую оценку качества предоставляемых услуг!"};

        private string[] YandexPolina = {
            "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/136988881857/reviews/?ll=55.946700%2C54.730911&z=13",
            "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/56397474575/reviews/?ll=52.395512%2C55.730124&z=14",
            "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/129005815772/reviews/?from=tabbar&ll=53.205785%2C56.845096&source=serp_navig&z=16",
            "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/66295943350/reviews/?from=tabbar&ll=50.128631%2C53.206851&source=serp_navig&z=12",
            "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/47336753648/reviews/?ll=49.410758%2C53.514368&z=8",
            "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/214804548889/reviews/?ll=48.395755%2C54.310993&z=13",
            "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/108075345724/reviews/?ll=46.005169%2C51.516137&z=14",
            "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/34366892390/reviews/?ll=82.913019%2C55.054826&z=14",
            "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/22667212166/reviews/?ll=44.512268%2C48.706922&z=15",
            "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/197293985189/reviews/?ll=47.792477%2C52.031598&tab=reviews&z=18.15",
            "" 
        };
        private string[] YandexMinislam = {
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/113157496143/reviews/?from=tabbar&ll=49.125393%2C55.793955&source=serp_navig&z=16",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/177742980083/reviews/?indoorLevel=1&ll=30.316176%2C59.936633&tab=reviews&z=17.93",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/131102774487/reviews/?from=tabbar&ll=55.943141%2C53.646386&source=serp_navig&z=16",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/66508441008/reviews/?from=tabbar&ll=47.253549%2C56.142118&source=serp_navig&z=16",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/147924753017/reviews/?from=tabbar&ll=92.867364%2C56.007428&source=serp_navig&z=16",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/239088643733/reviews/?from=tabbar&ll=43.979890%2C56.310208&source=serp_navig&z=13",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/143980747105/reviews/?ll=39.703254%2C47.222443&z=15",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/20119723178/reviews/?ll=54.249179%2C56.089754&z=13",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/166935251693/reviews/?ll=37.663215%2C55.746113&z=10",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/163317269224/reviews/?ll=49.160053%2C55.827494&tab=reviews&z=18.26",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/192431969643/?ll=37.337420%2C44.899861&z=19.15" 
        };
        private string[] YandexDiana = {
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/240149676447/reviews/?from=tabbar&ll=52.302458%2C54.903179&source=serp_navig&z=16",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/19079962301/reviews/?ll=39.718416%2C43.588183&tab=reviews&z=19.68",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/104764196657/reviews/?from=tabbar&ll=53.473891%2C54.482091&source=serp_navig&z=15",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/213670361628/reviews/?ll=38.976109%2C45.032668&z=14",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/198608088894/reviews/?ll=37.636645%2C55.759933&z=15",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/78064388820/reviews/?ll=60.627406%2C56.839605&z=17",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/211128491904/reviews/?ll=56.265082%2C57.999407&z=13",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/156110516036/reviews/?ll=39.891792%2C57.629590&z=17",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/30458075243/reviews/?ll=65.562777%2C57.151515&z=18",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/58675881355/reviews/?ll=20.487765%2C54.718815&z=18",
                "https://yandex.ru/maps/org/mezhdunarodnoye_yuridicheskoye_byuro/199329670459/reviews/?ll=37.440119%2C55.892743&tab=reviews&z=17"
        };

        private string[] dGisPolina = { };
        private string[] dGisMinislam = { };
        private string[] dGisDiana = { };
        
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

        private void YandexBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] urls = { };

            switch (MainWindow.UserID)
            {
                case 1:
                    urls = YandexPolina;
                    break;
                case 2:
                    urls = YandexMinislam;
                    break;
                case 3:
                    urls = YandexDiana;
                    break;
            }
            foreach (string url in urls)
            {            
                try
                {
                    Process.Start(new ProcessStartInfo() { FileName = url, UseShellExecute = true });
                }
                catch
                {
                }
            }
        }

        private void dGISBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OtzovikBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZoonBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FlampBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
