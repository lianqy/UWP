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
using Windows.UI.Popups;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.DataTransfer;
using SQLitePCL;
using Windows.ApplicationModel;
using System.Net.Http;
using Newtonsoft.Json.Linq;


// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Booklists
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SelectedBookList2 : Page
    {
        public SelectedBookList2()
        {
            this.InitializeComponent();
            this.BooklistViewModel = new MyTodos.ViewModels.BookListViewModel();
            this.bookViewModel = new MyTodos.ViewModels.BookViewModel();
            getFirstPicture();
        }
        MyTodos.ViewModels.BookListViewModel BooklistViewModel { get; set; }
        MyTodos.ViewModels.BookViewModel bookViewModel { get; set; }

        string fid;
        private void ReadDatabase()
        {
            var conn2 = App.conn2;
            using (var mydb = conn2.Prepare(@"SELECT id, fid, title, description, classification, introduction, buy_link, completed, imagebyte, date FROM item2"))
            {
                while (SQLiteResult.ROW == mydb.Step())
                {
                    if (mydb[1].ToString() == fid) ///
                    {
                        byte[] mybytes = (byte[])mydb[8]; //
                        BitmapImage bitmapImage = new BitmapImage();
                        if (mydb[8] != null)
                        {
                            IRandomAccessStream stream = this.ConvertToRandomAccessStream(new MemoryStream(mybytes));
                            bitmapImage.SetSource(stream);
                        }
                        if (mydb[7].ToString() == "False")
                        {
                            bookViewModel.AddBook(true, mydb[0].ToString(), mydb[1].ToString(), mydb[2].ToString(), mydb[3].ToString(), mydb[4].ToString(), mydb[5].ToString(), mydb[6].ToString(), false, bitmapImage, mybytes, mydb[9].ToString());
                        }
                        else
                        {
                            bookViewModel.AddBook(true, mydb[0].ToString(), mydb[1].ToString(), mydb[2].ToString(), mydb[3].ToString(), mydb[4].ToString(), mydb[5].ToString(), mydb[6].ToString(), true, bitmapImage, mybytes, mydb[9].ToString());
                        }
                        /*
                         * int id, int tag, string title, string description, ImageSource cover, byte[] imagebyte, string date)
                         */
                    }
                }
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.GetType() == typeof(MyTodos.ViewModels.BookViewModel))
            {
                this.bookViewModel = (MyTodos.ViewModels.BookViewModel)(e.Parameter);
                fid = bookViewModel.getfid();
                if (fid == null)
                {
                    fid = this.BooklistViewModel.SelectedItem.id;
                    this.bookViewModel.setfid(this.BooklistViewModel.SelectedItem.id);
                }
            }
            else
            {
                this.BooklistViewModel = (MyTodos.ViewModels.BookListViewModel)(e.Parameter);
                this.bookViewModel = new MyTodos.ViewModels.BookViewModel(); ///
                this.bookViewModel.setfid(this.BooklistViewModel.SelectedItem.id);
                fid = this.BooklistViewModel.SelectedItem.id;
                ReadDatabase();
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

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SelectedBook), bookViewModel);
        }

        private void TodoItem_ItemClicked(object sender, ItemClickEventArgs e)
        {
            bookViewModel.SelectedItem = (MyTodos.Models.Book)(e.ClickedItem);
            if (InlineToDoItemViewGrid.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                Frame.Navigate(typeof(SelectedBook), bookViewModel);
            }
            else
            {
                Title1.Text = bookViewModel.SelectedItem.title;
                Description1.Text = bookViewModel.SelectedItem.description;
                Content1.Text = bookViewModel.SelectedItem.introduction;
                bgimage.Source = bookViewModel.SelectedItem.cover;
                tagbox.Text = bookViewModel.SelectedItem.classification;
                Date1.Date = DateTime.Parse(bookViewModel.SelectedItem.date.ToString());

                mybutton.Content = "Update";
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
                bookViewModel.AddBook(false, "a", fid, Title1.Text, Description1.Text, tagbox.Text, Content1.Text, "www.baidu.com", false, bgimage.Source, imagebyte, Date1.Date.ToString());
                Title1.Text = "";
                Description1.Text = "";
                Content1.Text = "";
                //Date1.Date = DateTime.Now.Date;
                Frame.Navigate(typeof(SelectedBookList2), bookViewModel);
            }
            else
            {
                var i = new MessageDialog("Updated").ShowAsync();
                bookViewModel.UpdateBook(Title1.Text, Description1.Text, tagbox.Text, Content1.Text, "www.baidu.com", bookViewModel.SelectedItem.completed, bgimage.Source, imagebyte, Date1.Date.ToString());
                Title1.Text = "";
                Description1.Text = "";
                Content1.Text = "";
                //Date1 = DateTime.Now.ToString();
                Frame.Navigate(typeof(SelectedBookList2), bookViewModel);
            }
        }

        private void MenuFlyoutItem_Click_edit(object sender, RoutedEventArgs e)
        {
            dynamic ori = e.OriginalSource;
            bookViewModel.SelectedItem = (MyTodos.Models.Book)ori.DataContext;
            Frame.Navigate(typeof(SelectedBook), bookViewModel); //
        }

        private void MenuFlyoutItem_Click_remove(object sender, RoutedEventArgs e)
        {
            if (bookViewModel.SelectedItem != null)
            {
                dynamic ori = e.OriginalSource;
                bookViewModel.SelectedItem = (MyTodos.Models.Book)ori.DataContext;
                bookViewModel.RemoveBook(bookViewModel.SelectedItem.id);
                Frame.Navigate(typeof(SelectedBookList2), bookViewModel);
            }
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            dynamic context = e.OriginalSource;
            bookViewModel.SelectedItem = (MyTodos.Models.Book)context.DataContext;
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.DataRequested);
            DataTransferManager.ShowShareUI();
        }

        private void DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.Properties.Title = bookViewModel.SelectedItem.title;
            request.Data.Properties.Description = bookViewModel.SelectedItem.description;
            request.Data.SetBitmap(Windows.Storage.Streams.RandomAccessStreamReference.CreateFromUri(
                                                            new Uri("ms-appx:///Assets/flower.jpg")));
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            int num = bookViewModel.AllItems.Count;
            for (int i = 0; i < num; i ++)
            {
                MyTodos.DBModels.DBViewModels.UpdateDatabase(bookViewModel.AllItems[i]);
            }
            Frame.Navigate(typeof(MainPage), bookViewModel);
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

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
