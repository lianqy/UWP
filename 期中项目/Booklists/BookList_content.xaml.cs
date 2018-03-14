using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Booklists
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BookList_content : Page
    {
        public BookList_content()
        {
            this.InitializeComponent();
            this.ViewModel = new MyTodos.ViewModels.BookListViewModel();
            getFirstPicture();
        }

        MyTodos.ViewModels.BookListViewModel ViewModel { get; set; }
        byte[] imagebyte;

        private async void getFirstPicture()
        {
            StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets");
            StorageFile file = await folder.GetFileAsync("book.jpg");
            if (file != null)
            {
                var fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                using (var dataReader = new DataReader(fileStream))
                {
                    var bytes = new byte[fileStream.Size];
                    await dataReader.LoadAsync((uint)fileStream.Size);
                    dataReader.ReadBytes(bytes);
                    imagebyte = bytes;
                }

            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.GetType() == typeof(MyTodos.ViewModels.BookListViewModel))
            {
                this.ViewModel = (MyTodos.ViewModels.BookListViewModel)(e.Parameter);
            }

            /*Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                 AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                 AppViewBackButtonVisibility.Collapsed;
            }*/

            if (ViewModel.SelectedItem != null)
            {
                bgimage1.Source = ViewModel.SelectedItem.cover;
                imagebyte = ViewModel.SelectedItem.imagebyte;
                Title1.Text = ViewModel.SelectedItem.title;
                Details1.Text = ViewModel.SelectedItem.description;
                int tag_num = ViewModel.SelectedItem.tag;
                if (tag_num == 1)
                {
                    tag.Icon = new SymbolIcon(Symbol.OutlineStar);
                    tag.Label = "收藏";
                }
                else if (tag_num == 2)
                {
                    tag.Icon = new SymbolIcon(Symbol.Bullets);
                    tag.Label = "已读";
                }
                else if (tag_num == 3)
                {
                    tag.Icon = new SymbolIcon(Symbol.Library);
                    tag.Label = "计划";
                }
            }
        }

        private async void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openFile = new FileOpenPicker();
            openFile.ViewMode = PickerViewMode.Thumbnail;
            openFile.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openFile.FileTypeFilter.Add(".jpg");
            openFile.FileTypeFilter.Add(".jpeg");
            openFile.FileTypeFilter.Add(".png");
            StorageFile file = await openFile.PickSingleFileAsync();
            if (file != null)
            {
                var fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                using (var dataReader = new DataReader(fileStream))
                {
                    var bytes = new byte[fileStream.Size];
                    await dataReader.LoadAsync((uint)fileStream.Size);
                    dataReader.ReadBytes(bytes);
                    imagebyte = bytes;
                    bgimage1.Source = LoadThePicture(bytes);
                    if (ViewModel.SelectedItem != null)
                    {
                        ViewModel.SelectedItem.imagebyte = bytes;
                    }
                }

            }
        }

        private IRandomAccessStream ConvertToRandomAccessStream(MemoryStream memoryStream)
        {

            var randomAccessStream = new InMemoryRandomAccessStream();
            var outputStream = randomAccessStream.GetOutputStreamAt(0);
            var wrStr = outputStream.AsStreamForWrite();
            wrStr.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
            wrStr.Flush();
            return randomAccessStream;

        }

        public ImageSource LoadThePicture(byte[] mybytes)
        {
            try
            {

                BitmapImage bitmapImage = new BitmapImage();
                IRandomAccessStream stream = this.ConvertToRandomAccessStream(new MemoryStream(mybytes));
                bitmapImage.SetSource(stream);

                return bitmapImage;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (Title1.Text.Trim() == String.Empty)
            {
                var i = new MessageDialog("标题不能为空").ShowAsync();
                flag = false;
            }
            if (Details1.Text.Trim() == String.Empty)
            {
                var i = new MessageDialog("细节不能为空").ShowAsync();
                flag = false;
            }
            if (tag.Label == "Tag")
            {
                var i = new MessageDialog("请选择书单类型").ShowAsync();
                flag = false;
            }
            if (flag && ViewModel.SelectedItem == null)
            {
                var i = new MessageDialog("Created").ShowAsync();
                if (tag.Label == "收藏")
                {
                    ViewModel.AddBookList("a", 1, Title1.Text, Details1.Text, bgimage1.Source, imagebyte, DateTime.Now.ToString());
                }
                else if (tag.Label == "已读")
                {
                    ViewModel.AddBookList("a", 2, Title1.Text, Details1.Text, bgimage1.Source, imagebyte, DateTime.Now.ToString());
                }
                else if (tag.Label == "计划")
                {
                    ViewModel.AddBookList("a", 3, Title1.Text, Details1.Text, bgimage1.Source, imagebyte, DateTime.Now.ToString());
                }
                Title1.Text = "";
                Details1.Text = "";
                Frame.Navigate(typeof(MainPage), ViewModel);
            } else if (flag && ViewModel.SelectedItem != null) {
                var i = new MessageDialog("Updated").ShowAsync();
                if (tag.Label == "收藏")
                {
                    ViewModel.UpdateBookList(ViewModel.SelectedItem.id, 1, Title1.Text, Details1.Text, bgimage1.Source, imagebyte, DateTime.Now.ToString());
                }
                else if (tag.Label == "已读")
                {
                    ViewModel.UpdateBookList(ViewModel.SelectedItem.id, 2, Title1.Text, Details1.Text, bgimage1.Source, imagebyte, DateTime.Now.ToString());
                }
                else if (tag.Label == "计划")
                {
                    ViewModel.UpdateBookList(ViewModel.SelectedItem.id, 3, Title1.Text, Details1.Text, bgimage1.Source, imagebyte, DateTime.Now.ToString());
                }
                Title1.Text = "";
                Details1.Text = "";
                Frame.Navigate(typeof(MainPage), ViewModel);
            }
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            tag.Icon = new SymbolIcon(Symbol.OutlineStar);
            tag.Label = "收藏";
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            tag.Icon = new SymbolIcon(Symbol.Bullets);
            tag.Label = "已读";
        }

        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e)
        {
            tag.Icon = new SymbolIcon(Symbol.Library);
            tag.Label = "计划";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Title1.Text = "";
            Details1.Text = "";
            Frame.Navigate(typeof(MainPage), ViewModel);
        }
    }
}
