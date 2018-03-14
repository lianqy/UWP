using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyTodos.Models;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.UI.Core;
using SQLitePCL;
using System.Text;
using Windows.Graphics.Imaging;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using System.Net.Http;
using Newtonsoft.Json.Linq;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Booklists
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new MyTodos.ViewModels.BookListViewModel();
            ReadDatabase(); //
            getFirstPicture();
        }

        private void ReadDatabase()
        {
            var conn = App.conn;
            using (var mydb = conn.Prepare(@"SELECT id, tag, title, description, imagebyte, date FROM item"))
            {
                while (SQLiteResult.ROW == mydb.Step())
                {
                    byte[] mybytes = (byte[])mydb[4];
                    BitmapImage bitmapImage = new BitmapImage();
                    if(mybytes != null)
                    {
                        IRandomAccessStream stream = this.ConvertToRandomAccessStream(new MemoryStream(mybytes));
                        bitmapImage.SetSource(stream);
                    }
                    ViewModel.AddBookList(mydb[0].ToString(), int.Parse(mydb[1].ToString()), mydb[2].ToString(), mydb[3].ToString(), bitmapImage, mybytes, mydb[5].ToString());
                    /*
                     * int id, int tag, string title, string description, ImageSource cover, byte[] imagebyte, string date)
                     */
                }
            }
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
                    bgimage.Source = LoadThePicture(bytes);
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
            dynamic ori = e.OriginalSource;
            ViewModel.SelectedItem = (MyTodos.Models.BookList)ori.DataContext;
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
            if (tag.Label == "Tag")
            {
                var i = new MessageDialog("请选择书单类型").ShowAsync();
                flag = false;
            }
            if (flag)
            {
                var i = new MessageDialog("Created").ShowAsync();
                if (tag.Label == "收藏")
                {
                    ViewModel.AddBookList("a", 1, Title.Text, Details.Text, bgimage.Source, imagebyte, DateTime.Now.ToString());
                }
                else if (tag.Label == "已读")
                {
                    ViewModel.AddBookList("a", 2, Title.Text, Details.Text, bgimage.Source, imagebyte, DateTime.Now.ToString());
                }
                else if (tag.Label == "计划")
                {
                    ViewModel.AddBookList("a", 3, Title.Text, Details.Text, bgimage.Source, imagebyte, DateTime.Now.ToString());
                }
                Title.Text = "";
                Details.Text = "";
                Frame.Navigate(typeof(MainPage), ViewModel);
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        { ////////////TODO
            if (e.Parameter.GetType() == typeof(MyTodos.ViewModels.BookListViewModel))
            {
                this.ViewModel = (MyTodos.ViewModels.BookListViewModel)(e.Parameter);
            }
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Bounds.Width < 800)
            {
                Frame.Navigate(typeof(BookList_content), ViewModel);
            }
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            dynamic ori = e.OriginalSource;
            ViewModel.SelectedItem = (MyTodos.Models.BookList)ori.DataContext;
            ViewModel.RemoveBookList(ViewModel.SelectedItem.id);
            Frame.Navigate(typeof(MainPage), ViewModel);
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            dynamic ori = e.OriginalSource;
            ViewModel.SelectedItem = (MyTodos.Models.BookList)ori.DataContext;
            Frame.Navigate(typeof(BookList_content), ViewModel);
        }

        private void ToDoListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.SelectedItem = (MyTodos.Models.BookList)(e.ClickedItem);
            if (ViewModel.SelectedItem.tag != 3)
            {
                Frame.Navigate(typeof(SelectedBookList), ViewModel);
            } else
            {
                Frame.Navigate(typeof(SelectedBookList2), ViewModel);
            }
        }

        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e)
        {
            tag.Icon = new SymbolIcon(Symbol.OutlineStar);
            tag.Label = "收藏";
        }

        private void MenuFlyoutItem_Click_3(object sender, RoutedEventArgs e)
        {
            tag.Icon = new SymbolIcon(Symbol.Bullets);
            tag.Label = "已读";
        }

        private void MenuFlyoutItem_Click_4(object sender, RoutedEventArgs e)
        {
            tag.Icon = new SymbolIcon(Symbol.Library);
            tag.Label = "计划";
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            book(searchBox.Text);
        }
        private async void book(string title)
        {
            if (title == "")
            {
                var i = new MessageDialog("请输入查询内容！").ShowAsync();
            }
            else
            {
                string url = "https://api.douban.com/v2/book/search?q=" + title + "&fields=summary";
                HttpClient client = new HttpClient();
                string result = await client.GetStringAsync(url);
                //get result
                JObject ob = JObject.Parse(result);
                if (ob["count"].ToString() == "20")
                {
                    var description = ob["books"];
                    foreach (JToken des in description.Children())
                    {
                        var des1 = des as JProperty;
                        //if(des1 != null)
                        //{
                        var xi = new MessageDialog(des["summary"].ToString()).ShowAsync();
                        break;
                        //}
                    }
                }
                else
                {
                    var i = new MessageDialog("查询不到有关书籍信息").ShowAsync();
                }
            }
        }
    }
}
