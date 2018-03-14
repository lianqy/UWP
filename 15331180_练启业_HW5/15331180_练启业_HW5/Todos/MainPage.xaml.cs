using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.ApplicationModel.DataTransfer;
using Todos.Models;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Todos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
            this.ViewModel = new ViewModels.TodoItemViewModel();

            //   var xmlDoc = TileService.CreateTiles(ViewModel.AllItems[0]);

            //   var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            //   TileNotification notification = new TileNotification(xmlDoc);
            //   updater.Update(notification);
            Tile(ViewModel);
        }

        ViewModels.TodoItemViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Tile(ViewModel);
            if (e.Parameter.GetType() == typeof(ViewModels.TodoItemViewModel))
            {
                this.ViewModel = (ViewModels.TodoItemViewModel)(e.Parameter);
            }
        }

        private void TodoItem_ItemClicked(object sender, ItemClickEventArgs e)
        {
            ViewModel.SelectedItem = (Models.TodoItem)(e.ClickedItem);
            if (Window.Current.Bounds.Width < 800)
            {
                Frame.Navigate(typeof(NewPage), ViewModel);
            }
            else
            {
                Title.Text = ViewModel.SelectedItem.title;
                Details.Text = ViewModel.SelectedItem.description;
                MyDate.Date = ViewModel.SelectedItem.date;
                createButton.Content = "Update";
            }
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if(Window.Current.Bounds.Width < 800)
            {
                if (ViewModel.SelectedItem != null)
                    ViewModel.SelectedItem = null;
                Frame.Navigate(typeof(NewPage), ViewModel);
            }
              //  Frame.Navigate(typeof(NewPage), ViewModel);
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem == null)
            {
                bool flag = true;
                if (Title.Text.Trim() == String.Empty)
                {
                    var i = new MessageDialog("标题不能为空").ShowAsync();
                    flag = false;
                }
                if (Details.Text.Trim() == String.Empty)
                {
                    var i = new MessageDialog("细节不能为空").ShowAsync();
                    flag = false;
                }
                /*  if (MyDate.Date > DateTimeOffset.Now)
                  {
                      var i = new MessageDialog("无效日期").ShowAsync();
                      flag = false;
                  }*/
                if (flag)
                {
                    var i = new MessageDialog("Created").ShowAsync();
                    ViewModel.AddTodoItem(Title.Text, Details.Text, MyDate.Date.DateTime);
                    Title.Text = "";
                    Details.Text = "";
                    Frame.Navigate(typeof(MainPage), ViewModel);
                    Tile(ViewModel);
                }
            } else
            {
                ViewModel.SelectedItem.rewrite_title(Title.Text);
                ViewModel.SelectedItem.rewrite_details(Details.Text);
                ViewModel.SelectedItem.rewrite_date(MyDate.Date.DateTime);
                Frame.Navigate(typeof(MainPage), ViewModel);
                Tile(ViewModel);
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedItem = null;
            Frame.Navigate(typeof(MainPage), ViewModel);
        }


        internal static void Tile(ViewModels.TodoItemViewModel View)
        {
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueueForSquare150x150(true);
            updater.EnableNotificationQueueForSquare310x310(true);
            updater.EnableNotificationQueueForWide310x150(true);
            updater.EnableNotificationQueue(true);
            foreach (var n in View.AllItems)
            {
                Windows.Data.Xml.Dom.XmlDocument doc = new Windows.Data.Xml.Dom.XmlDocument();
                doc = TileService.CreateTiles(n);
                TileNotification tileNotification = new TileNotification(doc);
                updater.Update(tileNotification);
            }

        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            dynamic ori = e.OriginalSource;
            ViewModel.SelectedItem = (Models.TodoItem)ori.DataContext;
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
            DataTransferManager.ShowShareUI();
        }

        async void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            var deferral = args.Request.GetDeferral();
            var photo = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/background.jpg"));
            request.Data.Properties.Title = ViewModel.SelectedItem.title;
            string str = ViewModel.SelectedItem.description;
            request.Data.SetText(str);
            request.Data.SetStorageItems(new List<StorageFile> { photo });
            deferral.Complete();

        }


    }
}
