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
    public sealed partial class SelectedBook : Page
    {
        public SelectedBook()
        {
            this.InitializeComponent();
            this.bookViewModel = new MyTodos.ViewModels.BookViewModel();
            getFirstPicture();
        }
        MyTodos.ViewModels.BookViewModel bookViewModel { get; set; }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            if(bookViewModel.SelectedItem != null)
            {
                bookViewModel.RemoveBook(bookViewModel.SelectedItem.id);
                Frame.Navigate(typeof(SelectedBookList), bookViewModel);
            }
        }
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
            if (e.Parameter.GetType() == typeof(MyTodos.ViewModels.BookViewModel))
            {
                this.bookViewModel = (MyTodos.ViewModels.BookViewModel)(e.Parameter);
            }

            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                 AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                 AppViewBackButtonVisibility.Collapsed;
            }

            if (bookViewModel.SelectedItem != null)
            {
                bgimage1.Source = bookViewModel.SelectedItem.cover;
                tagbox.Text = bookViewModel.SelectedItem.classification;
                Title1.Text = bookViewModel.SelectedItem.title;
                Description1.Text = bookViewModel.SelectedItem.description;
                Content1.Text = bookViewModel.SelectedItem.introduction;
                ///date TODO
                Date1.Date = DateTime.Parse(bookViewModel.SelectedItem.date.ToString());
                mybutton.Content = "Update";
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
                    if (bookViewModel.SelectedItem != null)
                    {
                        bookViewModel.SelectedItem.imagebyte = bytes;
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
            if (Description1.Text.Trim() == String.Empty)
            {
                var i = new MessageDialog("细节不能为空").ShowAsync();
                flag = false;
            }
            if (Content1.Text.Trim() == String.Empty)
            {
                var i = new MessageDialog("内容不能为空").ShowAsync();
                flag = false;
            }
            if (flag && bookViewModel.SelectedItem == null)
            {
                var i = new MessageDialog("Created").ShowAsync();
                bookViewModel.AddBook(false, "a", bookViewModel.SelectedItem.fid, Title1.Text, Description1.Text, tagbox.Text, Content1.Text, "www.baidu.com", false, bgimage1.Source, imagebyte, Date1.Date.ToString());
                Title1.Text = "";
                Description1.Text = "";
                Content1.Text = "";
                Date1.Date = DateTime.Now.Date; //
                Frame.Navigate(typeof(SelectedBookList), bookViewModel);
            }
            else
            {
                var i = new MessageDialog("Updated").ShowAsync();
                bookViewModel.UpdateBook(Title1.Text, Description1.Text, tagbox.Text, Content1.Text, "www.baidu.com", bookViewModel.SelectedItem.completed, bgimage1.Source, imagebyte, Date1.Date.ToString());
                Title1.Text = "";
                Description1.Text = "";
                Content1.Text = "";
                //Date1 = DateTime.Now.ToString();
                Frame.Navigate(typeof(SelectedBookList), bookViewModel);
            }
        }
        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
