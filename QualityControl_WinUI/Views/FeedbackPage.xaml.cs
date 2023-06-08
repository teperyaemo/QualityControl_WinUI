// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Text;
using Windows.Storage.Pickers;
using Windows.Storage;
using QualityControl_WinUI.Helpers;
using ABI.System;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace QualityControl_WinUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FeedbackPage : Page
    {
        public static int iter = 1;
        public FeedbackPage()
        {
            this.InitializeComponent();
            CalendarDP.Date = getYesterdayWorkDay();
        }
        private async void PickFilesButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous returned file name, if it exists, between iterations of this scenario
            PickFilesOutputTextBlock.Text = "";

            // Create a file picker
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var window = WindowHelper.GetWindowForElement(this);
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Initialize the file picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            // Set options for your file picker
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.Downloads;
            openPicker.FileTypeFilter.Add("*");

            // Open the picker for the user to pick a file
            IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();
            if (files.Count > 0)
            {
                StringBuilder output = new StringBuilder("Picked files:\n");
                foreach (StorageFile file in files)
                {
                    output.Append(file.Name + "\n");
                }
                PickFilesOutputTextBlock.Text = output.ToString();

                CountOfRemovedLinksTB.Text = $"{reading(files)} duplicate links were deleted";
            }
            else
            {
                PickFilesOutputTextBlock.Text = "Operation cancelled.";
            }
        }

        private void OpenLinkBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = iter; i < DataListView.Items.Count; ++i)
            {

                try
                {
                    string url = DataListView.Items[i-1].ToString();
                    //System.Diagnostics.Process.Start((string)DataListView.Items[i-1]);
                    Process.Start(new ProcessStartInfo() { FileName = url, UseShellExecute = true });
                    if (i % CountSlider.Value == 0)
                    {
                        OpenLinkBtn.Content = "Open following links";
                        iter = i + 1;
                        break;
                    }
                }
                catch
                {
                    OpenLinkBtn.Content = "All links are open";
                    OpenLinkBtn.IsEnabled = false;
                }
            }
        }

        public int reading(IReadOnlyList<StorageFile> files)
        {
            List<string> distinct = null;
            int countoflinks = 0;
            string[] keys = new string[] { "https://8993132.amocrm.ru/leads/detail/" };
            foreach (StorageFile file in files) 
            {
                IEnumerable<string> lines = File.ReadLines(file.Path);
                List<string> links = new List<string>();
                foreach (string line in lines)
                {
                    KeyWords(line, keys, ref links);
                }
                countoflinks += links.Count;
                if (distinct == null) { distinct = links; }
                else distinct.AddRange(links);
            }

            DataListView.ItemsSource = distinct.Distinct().ToList();
            return countoflinks - DataListView.Items.Count;
        }

        public static void KeyWords(string text, string[] keys, ref List<string> links)
        {
            string[] words = text.Split("\"".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
                foreach (string key in keys)
                    if (word.ToLower().Contains(key.ToLower()))
                        links.Add(word);
        }

        private void OpenUrlBtn_Click(object sender, RoutedEventArgs e)
        {
            System.DateTimeOffset PassDate = (System.DateTimeOffset)CalendarDP.Date;
            string datefrom = PassDate.ToString("dd'.'MM'.'yyyy");
            string dateto = PassDate.ToString("dd'.'MM'.'yyyy");
            string url = $"https://8993132.amocrm.ru/todo/list/?filter_date_switch=closed&filter%5Bmain_user%5D%5B%5D=7269051&filter%5Bmain_user%5D%5B%5D=8304381&filter%5Bmain_user%5D%5B%5D=9267293&filter%5Bmain_user%5D%5B%5D=9513821&filter%5Bpipe%5D%5B1910761%5D%5B%5D=28882411&filter%5Bpipe%5D%5B1910761%5D%5B%5D=28882414&filter%5Bpipe%5D%5B1910761%5D%5B%5D=28882417&filter%5Bpipe%5D%5B1910761%5D%5B%5D=28882420&filter%5Bpipe%5D%5B1910761%5D%5B%5D=28882495&filter%5Bpipe%5D%5B1910761%5D%5B%5D=28882498&filter%5Bpipe%5D%5B1910761%5D%5B%5D=28885108&filter%5Bpipe%5D%5B1910761%5D%5B%5D=28945231&filter%5Bpipe%5D%5B1910761%5D%5B%5D=53873233&filter%5Bpipe%5D%5B3390778%5D%5B%5D=33858421&filter%5Bpipe%5D%5B3390778%5D%5B%5D=33858424&filter%5Bpipe%5D%5B3390778%5D%5B%5D=33858427&filter%5Bpipe%5D%5B3390778%5D%5B%5D=33858430&filter%5Bpipe%5D%5B3390778%5D%5B%5D=33930043&filter%5Bpipe%5D%5B3390778%5D%5B%5D=33930046&filter%5Bpipe%5D%5B3390778%5D%5B%5D=33930049&filter%5Bpipe%5D%5B3390778%5D%5B%5D=33930052&filter%5Bpipe%5D%5B3390778%5D%5B%5D=33930055&filter%5Bpipe%5D%5B3390778%5D%5B%5D=53873317&filter%5Bpipe%5D%5B3495354%5D%5B%5D=34574406&filter%5Bpipe%5D%5B3495354%5D%5B%5D=34574409&filter%5Bpipe%5D%5B3495354%5D%5B%5D=34574412&filter%5Bpipe%5D%5B3495354%5D%5B%5D=34574415&filter%5Bpipe%5D%5B3495354%5D%5B%5D=34574574&filter%5Bpipe%5D%5B3495354%5D%5B%5D=34574577&filter%5Bpipe%5D%5B3495354%5D%5B%5D=34574580&filter%5Bpipe%5D%5B3495354%5D%5B%5D=34574583&filter%5Bpipe%5D%5B3495354%5D%5B%5D=34574586&filter%5Bpipe%5D%5B3593745%5D%5B%5D=35234160&filter%5Bpipe%5D%5B3593745%5D%5B%5D=35234163&filter%5Bpipe%5D%5B3593745%5D%5B%5D=35234166&filter%5Bpipe%5D%5B3593745%5D%5B%5D=35234169&filter%5Bpipe%5D%5B3593745%5D%5B%5D=35234172&filter%5Bpipe%5D%5B3593745%5D%5B%5D=35234175&filter%5Bpipe%5D%5B3593745%5D%5B%5D=35234178&filter%5Bpipe%5D%5B3593745%5D%5B%5D=35234181&filter%5Bpipe%5D%5B3593745%5D%5B%5D=35234184&filter%5Bpipe%5D%5B4074816%5D%5B%5D=38580441&filter%5Bpipe%5D%5B4074816%5D%5B%5D=38580444&filter%5Bpipe%5D%5B4074816%5D%5B%5D=38580447&filter%5Bpipe%5D%5B4074816%5D%5B%5D=38580450&filter%5Bpipe%5D%5B4074816%5D%5B%5D=38584131&filter%5Bpipe%5D%5B4074816%5D%5B%5D=38584134&filter%5Bpipe%5D%5B4074816%5D%5B%5D=38584137&filter%5Bpipe%5D%5B4074816%5D%5B%5D=38584140&filter%5Bpipe%5D%5B4074816%5D%5B%5D=38584143&filter%5Bpipe%5D%5B4074816%5D%5B%5D=54810349&filter%5Bpipe%5D%5B4329117%5D%5B%5D=40372227&filter%5Bpipe%5D%5B4329117%5D%5B%5D=40372230&filter%5Bpipe%5D%5B4329117%5D%5B%5D=40372233&filter%5Bpipe%5D%5B4329117%5D%5B%5D=40372236&filter%5Bpipe%5D%5B4329117%5D%5B%5D=40372239&filter%5Bpipe%5D%5B4329117%5D%5B%5D=40372242&filter%5Bpipe%5D%5B4329117%5D%5B%5D=40372245&filter%5Bpipe%5D%5B4329117%5D%5B%5D=40372248&filter%5Bpipe%5D%5B4329117%5D%5B%5D=40372251&filter%5Bpipe%5D%5B4492152%5D%5B%5D=41530092&filter%5Bpipe%5D%5B4492152%5D%5B%5D=41530095&filter%5Bpipe%5D%5B4492152%5D%5B%5D=41530098&filter%5Bpipe%5D%5B4492152%5D%5B%5D=41530101&filter%5Bpipe%5D%5B4492152%5D%5B%5D=41530116&filter%5Bpipe%5D%5B4492152%5D%5B%5D=41530119&filter%5Bpipe%5D%5B4492152%5D%5B%5D=41530122&filter%5Bpipe%5D%5B4492152%5D%5B%5D=41530125&filter%5Bpipe%5D%5B4492152%5D%5B%5D=41530128&filter%5Bpipe%5D%5B4492152%5D%5B%5D=51563601&filter%5Bpipe%5D%5B4589772%5D%5B%5D=42233640&filter%5Bpipe%5D%5B4589772%5D%5B%5D=42233643&filter%5Bpipe%5D%5B4589772%5D%5B%5D=42233646&filter%5Bpipe%5D%5B4589772%5D%5B%5D=42233649&filter%5Bpipe%5D%5B4589772%5D%5B%5D=42233832&filter%5Bpipe%5D%5B4589772%5D%5B%5D=42233835&filter%5Bpipe%5D%5B4589772%5D%5B%5D=42233838&filter%5Bpipe%5D%5B4589772%5D%5B%5D=42233841&filter%5Bpipe%5D%5B4589772%5D%5B%5D=42233844&filter%5Bpipe%5D%5B4668654%5D%5B%5D=42804534&filter%5Bpipe%5D%5B4668654%5D%5B%5D=42804537&filter%5Bpipe%5D%5B4668654%5D%5B%5D=42804540&filter%5Bpipe%5D%5B4668654%5D%5B%5D=42804543&filter%5Bpipe%5D%5B4668654%5D%5B%5D=42805683&filter%5Bpipe%5D%5B4668654%5D%5B%5D=42805686&filter%5Bpipe%5D%5B4668654%5D%5B%5D=42805689&filter%5Bpipe%5D%5B4668654%5D%5B%5D=42805692&filter%5Bpipe%5D%5B4668654%5D%5B%5D=42805695&filter%5Bpipe%5D%5B4668654%5D%5B%5D=49187694&filter%5Bpipe%5D%5B4764054%5D%5B%5D=43483368&filter%5Bpipe%5D%5B4764054%5D%5B%5D=43483371&filter%5Bpipe%5D%5B4764054%5D%5B%5D=43483374&filter%5Bpipe%5D%5B4764054%5D%5B%5D=43483377&filter%5Bpipe%5D%5B4764054%5D%5B%5D=43483383&filter%5Bpipe%5D%5B4764054%5D%5B%5D=43483386&filter%5Bpipe%5D%5B4764054%5D%5B%5D=43483389&filter%5Bpipe%5D%5B4764054%5D%5B%5D=43483392&filter%5Bpipe%5D%5B4764054%5D%5B%5D=43483395&filter%5Bpipe%5D%5B4764054%5D%5B%5D=51896937&filter%5Bpipe%5D%5B5065464%5D%5B%5D=45543963&filter%5Bpipe%5D%5B5065464%5D%5B%5D=45543966&filter%5Bpipe%5D%5B5065464%5D%5B%5D=45543969&filter%5Bpipe%5D%5B5065464%5D%5B%5D=45543972&filter%5Bpipe%5D%5B5065464%5D%5B%5D=45544005&filter%5Bpipe%5D%5B5065464%5D%5B%5D=45544008&filter%5Bpipe%5D%5B5065464%5D%5B%5D=45544011&filter%5Bpipe%5D%5B5065464%5D%5B%5D=45544014&filter%5Bpipe%5D%5B5065464%5D%5B%5D=45544017&filter%5Bpipe%5D%5B5065464%5D%5B%5D=48914613&filter%5Bpipe%5D%5B5172192%5D%5B%5D=46316715&filter%5Bpipe%5D%5B5172192%5D%5B%5D=46316718&filter%5Bpipe%5D%5B5172192%5D%5B%5D=46316721&filter%5Bpipe%5D%5B5172192%5D%5B%5D=46316724&filter%5Bpipe%5D%5B5172192%5D%5B%5D=46316841&filter%5Bpipe%5D%5B5172192%5D%5B%5D=46316844&filter%5Bpipe%5D%5B5172192%5D%5B%5D=46316847&filter%5Bpipe%5D%5B5172192%5D%5B%5D=46316850&filter%5Bpipe%5D%5B5172192%5D%5B%5D=46316853&filter%5Bpipe%5D%5B5172192%5D%5B%5D=50177553&filter%5Bpipe%5D%5B5172195%5D%5B%5D=46316727&filter%5Bpipe%5D%5B5172195%5D%5B%5D=46316730&filter%5Bpipe%5D%5B5172195%5D%5B%5D=46316733&filter%5Bpipe%5D%5B5172195%5D%5B%5D=46316736&filter%5Bpipe%5D%5B5172195%5D%5B%5D=46316763&filter%5Bpipe%5D%5B5172195%5D%5B%5D=46316766&filter%5Bpipe%5D%5B5172195%5D%5B%5D=46316769&filter%5Bpipe%5D%5B5172195%5D%5B%5D=46316772&filter%5Bpipe%5D%5B5172195%5D%5B%5D=46316835&filter%5Bpipe%5D%5B5172195%5D%5B%5D=55426169&filter%5Bpipe%5D%5B5241975%5D%5B%5D=46812006&filter%5Bpipe%5D%5B5241975%5D%5B%5D=46812009&filter%5Bpipe%5D%5B5241975%5D%5B%5D=46812012&filter%5Bpipe%5D%5B5241975%5D%5B%5D=46812015&filter%5Bpipe%5D%5B5241975%5D%5B%5D=46812051&filter%5Bpipe%5D%5B5241975%5D%5B%5D=46812054&filter%5Bpipe%5D%5B5241975%5D%5B%5D=46812057&filter%5Bpipe%5D%5B5241975%5D%5B%5D=46812060&filter%5Bpipe%5D%5B5241975%5D%5B%5D=46812063&filter%5Bpipe%5D%5B5241975%5D%5B%5D=54713121&filter%5Bpipe%5D%5B5388936%5D%5B%5D=47868903&filter%5Bpipe%5D%5B5388936%5D%5B%5D=47868906&filter%5Bpipe%5D%5B5388936%5D%5B%5D=47868909&filter%5Bpipe%5D%5B5388936%5D%5B%5D=47868912&filter%5Bpipe%5D%5B5388936%5D%5B%5D=47868945&filter%5Bpipe%5D%5B5388936%5D%5B%5D=47868948&filter%5Bpipe%5D%5B5388936%5D%5B%5D=47868951&filter%5Bpipe%5D%5B5388936%5D%5B%5D=47868954&filter%5Bpipe%5D%5B5388936%5D%5B%5D=47868957&filter%5Bpipe%5D%5B5389179%5D%5B%5D=47870691&filter%5Bpipe%5D%5B5389179%5D%5B%5D=47870694&filter%5Bpipe%5D%5B5389179%5D%5B%5D=47870697&filter%5Bpipe%5D%5B5389179%5D%5B%5D=47870700&filter%5Bpipe%5D%5B5389179%5D%5B%5D=47870751&filter%5Bpipe%5D%5B5389179%5D%5B%5D=47870754&filter%5Bpipe%5D%5B5389179%5D%5B%5D=47870757&filter%5Bpipe%5D%5B5389179%5D%5B%5D=47870760&filter%5Bpipe%5D%5B5389179%5D%5B%5D=47870763&filter%5Bpipe%5D%5B5389179%5D%5B%5D=54322049&filter%5Bpipe%5D%5B5389182%5D%5B%5D=47870703&filter%5Bpipe%5D%5B5389182%5D%5B%5D=47870709&filter%5Bpipe%5D%5B5389182%5D%5B%5D=47870769&filter%5Bpipe%5D%5B5389182%5D%5B%5D=47870772&filter%5Bpipe%5D%5B5389182%5D%5B%5D=47870775&filter%5Bpipe%5D%5B5389182%5D%5B%5D=47870778&filter%5Bpipe%5D%5B5389182%5D%5B%5D=47870781&filter%5Bpipe%5D%5B5389182%5D%5B%5D=47870784&filter%5Bpipe%5D%5B5389182%5D%5B%5D=47870787&filter%5Bpipe%5D%5B5389182%5D%5B%5D=53873349&filter%5Bpipe%5D%5B5389185%5D%5B%5D=47870715&filter%5Bpipe%5D%5B5389185%5D%5B%5D=47870718&filter%5Bpipe%5D%5B5389185%5D%5B%5D=47870721&filter%5Bpipe%5D%5B5389185%5D%5B%5D=47870724&filter%5Bpipe%5D%5B5389185%5D%5B%5D=47870796&filter%5Bpipe%5D%5B5389185%5D%5B%5D=47870799&filter%5Bpipe%5D%5B5389185%5D%5B%5D=47870802&filter%5Bpipe%5D%5B5389185%5D%5B%5D=47870805&filter%5Bpipe%5D%5B5389185%5D%5B%5D=47870808&filter%5Bpipe%5D%5B5389185%5D%5B%5D=54712273&filter%5Bpipe%5D%5B5389188%5D%5B%5D=47870727&filter%5Bpipe%5D%5B5389188%5D%5B%5D=47870730&filter%5Bpipe%5D%5B5389188%5D%5B%5D=47870814&filter%5Bpipe%5D%5B5389188%5D%5B%5D=47870817&filter%5Bpipe%5D%5B5389188%5D%5B%5D=47870820&filter%5Bpipe%5D%5B5389188%5D%5B%5D=47870823&filter%5Bpipe%5D%5B5389188%5D%5B%5D=47870826&filter%5Bpipe%5D%5B5389188%5D%5B%5D=47870829&filter%5Bpipe%5D%5B5389188%5D%5B%5D=47870832&filter%5Bpipe%5D%5B5389191%5D%5B%5D=47870739&filter%5Bpipe%5D%5B5389191%5D%5B%5D=47870742&filter%5Bpipe%5D%5B5389191%5D%5B%5D=47870838&filter%5Bpipe%5D%5B5389191%5D%5B%5D=47870841&filter%5Bpipe%5D%5B5389191%5D%5B%5D=47870844&filter%5Bpipe%5D%5B5389191%5D%5B%5D=47870847&filter%5Bpipe%5D%5B5389191%5D%5B%5D=47870850&filter%5Bpipe%5D%5B5389191%5D%5B%5D=47870853&filter%5Bpipe%5D%5B5389191%5D%5B%5D=47870856&filter%5Bpipe%5D%5B5389191%5D%5B%5D=53873401&filter%5Bpipe%5D%5B5687520%5D%5B%5D=50028195&filter%5Bpipe%5D%5B5687520%5D%5B%5D=50028198&filter%5Bpipe%5D%5B5687520%5D%5B%5D=50028201&filter%5Bpipe%5D%5B5687520%5D%5B%5D=50028204&filter%5Bpipe%5D%5B5687520%5D%5B%5D=50028231&filter%5Bpipe%5D%5B5687520%5D%5B%5D=50028234&filter%5Bpipe%5D%5B5687520%5D%5B%5D=50028237&filter%5Bpipe%5D%5B5687520%5D%5B%5D=50028240&filter%5Bpipe%5D%5B5687520%5D%5B%5D=50028243&filter%5Bpipe%5D%5B5687523%5D%5B%5D=50028246&filter%5Bpipe%5D%5B5687523%5D%5B%5D=50028249&filter%5Bpipe%5D%5B5687523%5D%5B%5D=50028252&filter%5Bpipe%5D%5B5687523%5D%5B%5D=50028255&filter%5Bpipe%5D%5B5687523%5D%5B%5D=50028258&filter%5Bpipe%5D%5B5687523%5D%5B%5D=50028261&filter%5Bpipe%5D%5B5687523%5D%5B%5D=50028264&filter%5Bpipe%5D%5B5687523%5D%5B%5D=50028267&filter%5Bpipe%5D%5B5687523%5D%5B%5D=50028270&filter%5Bpipe%5D%5B5687526%5D%5B%5D=50028279&filter%5Bpipe%5D%5B5687526%5D%5B%5D=50028282&filter%5Bpipe%5D%5B5687526%5D%5B%5D=50028285&filter%5Bpipe%5D%5B5687526%5D%5B%5D=50028288&filter%5Bpipe%5D%5B5687526%5D%5B%5D=50028291&filter%5Bpipe%5D%5B5687526%5D%5B%5D=50028294&filter%5Bpipe%5D%5B5687526%5D%5B%5D=50028297&filter%5Bpipe%5D%5B5687526%5D%5B%5D=50028300&filter%5Bpipe%5D%5B5687526%5D%5B%5D=50028303&filter%5Bpipe%5D%5B5687529%5D%5B%5D=50028306&filter%5Bpipe%5D%5B5687529%5D%5B%5D=50028309&filter%5Bpipe%5D%5B5687529%5D%5B%5D=50028312&filter%5Bpipe%5D%5B5687529%5D%5B%5D=50028315&filter%5Bpipe%5D%5B5687529%5D%5B%5D=50028327&filter%5Bpipe%5D%5B5687529%5D%5B%5D=50028330&filter%5Bpipe%5D%5B5687529%5D%5B%5D=50028333&filter%5Bpipe%5D%5B5687529%5D%5B%5D=50028336&filter%5Bpipe%5D%5B5687529%5D%5B%5D=50028339&filter%5Bpipe%5D%5B5687529%5D%5B%5D=53873353&filter%5Bpipe%5D%5B5687532%5D%5B%5D=50028345&filter%5Bpipe%5D%5B5687532%5D%5B%5D=50028348&filter%5Bpipe%5D%5B5687532%5D%5B%5D=50028351&filter%5Bpipe%5D%5B5687532%5D%5B%5D=50028354&filter%5Bpipe%5D%5B5687532%5D%5B%5D=50028366&filter%5Bpipe%5D%5B5687532%5D%5B%5D=50028369&filter%5Bpipe%5D%5B5687532%5D%5B%5D=50028372&filter%5Bpipe%5D%5B5687532%5D%5B%5D=50028375&filter%5Bpipe%5D%5B5687532%5D%5B%5D=50028378&filter%5Bpipe%5D%5B5687535%5D%5B%5D=50028381&filter%5Bpipe%5D%5B5687535%5D%5B%5D=50028384&filter%5Bpipe%5D%5B5687535%5D%5B%5D=50028387&filter%5Bpipe%5D%5B5687535%5D%5B%5D=50028390&filter%5Bpipe%5D%5B5687535%5D%5B%5D=50028393&filter%5Bpipe%5D%5B5687535%5D%5B%5D=50028396&filter%5Bpipe%5D%5B5687535%5D%5B%5D=50028399&filter%5Bpipe%5D%5B5687535%5D%5B%5D=50028402&filter%5Bpipe%5D%5B5687535%5D%5B%5D=50028405&filter%5Bpipe%5D%5B5687538%5D%5B%5D=50028420&filter%5Bpipe%5D%5B5687538%5D%5B%5D=50028423&filter%5Bpipe%5D%5B5687538%5D%5B%5D=50028426&filter%5Bpipe%5D%5B5687538%5D%5B%5D=50028429&filter%5Bpipe%5D%5B5687538%5D%5B%5D=50028435&filter%5Bpipe%5D%5B5687538%5D%5B%5D=50028438&filter%5Bpipe%5D%5B5687538%5D%5B%5D=50028441&filter%5Bpipe%5D%5B5687538%5D%5B%5D=50028444&filter%5Bpipe%5D%5B5687538%5D%5B%5D=50028447&filter%5Bpipe%5D%5B6258157%5D%5B%5D=53886593&filter%5Bpipe%5D%5B6258157%5D%5B%5D=53886597&filter%5Bpipe%5D%5B6258157%5D%5B%5D=53886601&filter%5Bpipe%5D%5B6258157%5D%5B%5D=53886605&filter%5Bpipe%5D%5B6258157%5D%5B%5D=53886637&filter%5Bpipe%5D%5B6258157%5D%5B%5D=53886641&filter%5Bpipe%5D%5B6258157%5D%5B%5D=53886645&filter%5Bpipe%5D%5B6258157%5D%5B%5D=53886649&filter%5Bpipe%5D%5B6258157%5D%5B%5D=53886653&filter%5Btask_type%5D%5B%5D=1&filter%5Btask_type%5D%5B%5D=1580830&filter_date_from={datefrom}&filter_date_to={dateto}&filter%5Bpipe%5D%5B6637065%5D%5B%5D=56327081&filter%5Bpipe%5D%5B6637065%5D%5B%5D=56327085&filter%5Bpipe%5D%5B6637065%5D%5B%5D=56327089&filter%5Bpipe%5D%5B6637065%5D%5B%5D=56327093&filter%5Bpipe%5D%5B6637065%5D%5B%5D=56333981&filter%5Bpipe%5D%5B6637065%5D%5B%5D=56333985&filter%5Bpipe%5D%5B6637065%5D%5B%5D=56333989&filter%5Bpipe%5D%5B6637065%5D%5B%5D=56333993&filter%5Bpipe%5D%5B6637065%5D%5B%5D=56333997&filter%5Bpipe%5D%5B6637069%5D%5B%5D=56327097&filter%5Bpipe%5D%5B6637069%5D%5B%5D=56327101&filter%5Bpipe%5D%5B6637069%5D%5B%5D=56327105&filter%5Bpipe%5D%5B6637069%5D%5B%5D=56327109&filter%5Bpipe%5D%5B6637069%5D%5B%5D=56334005&filter%5Bpipe%5D%5B6637069%5D%5B%5D=56334009&filter%5Bpipe%5D%5B6637069%5D%5B%5D=56334013&filter%5Bpipe%5D%5B6637069%5D%5B%5D=56334017&filter%5Bpipe%5D%5B6637069%5D%5B%5D=56334021&filter%5Bpipe%5D%5B6637073%5D%5B%5D=56327113&filter%5Bpipe%5D%5B6637073%5D%5B%5D=56327117&filter%5Bpipe%5D%5B6637073%5D%5B%5D=56327121&filter%5Bpipe%5D%5B6637073%5D%5B%5D=56327125&filter%5Bpipe%5D%5B6637073%5D%5B%5D=56334029&filter%5Bpipe%5D%5B6637073%5D%5B%5D=56334033&filter%5Bpipe%5D%5B6637073%5D%5B%5D=56334037&filter%5Bpipe%5D%5B6637073%5D%5B%5D=56334041&filter%5Bpipe%5D%5B6637073%5D%5B%5D=56334045&useFilter=y";
            Process.Start(new ProcessStartInfo() { FileName = url, UseShellExecute = true });
        }

        private DateTime getYesterdayWorkDay()
        {
            if(DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                return DateTime.Now.AddDays(-3);
            }
            else return DateTime.Now.AddDays(-1);
        }

    }
}
