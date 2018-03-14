using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;
using MyTodos.ViewModels;
using MyTodos.Models;
using MyTodos.DBModels;
using SQLitePCL;
using Booklists;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System.IO;

namespace MyTodos.ViewModels
{
    class BookListViewModel
    {
        private ObservableCollection<Models.BookList> allItems = new ObservableCollection<Models.BookList>();
        public ObservableCollection<Models.BookList> AllItems { get { return this.allItems; } }

        private Models.BookList selectedItem = default(Models.BookList);
        public Models.BookList SelectedItem { get { return selectedItem; } set { this.selectedItem = value; } }

        public BookListViewModel()
        {
            /*
            var conn = App.conn;
            using (var mydb = conn.Prepare(@"SELECT id, tag, title, description, imagebyte, date FROM item"))
            {
                while (SQLiteResult.ROW == mydb.Step())
                {
                    byte[] mybytes = (byte[])mydb[4];
                    BitmapImage bitmapImage = new BitmapImage();
                    if (mybytes != null)
                    {
                        IRandomAccessStream stream = this.ConvertToRandomAccessStream(new MemoryStream(mybytes));
                        bitmapImage.SetSource(stream);
                    }
                    this.AddBookList(mydb[0].ToString(), int.Parse(mydb[1].ToString()), mydb[2].ToString(), mydb[3].ToString(), bitmapImage, mybytes, mydb[5].ToString());
                    /*
                     * int id, int tag, string title, string description, ImageSource cover, byte[] imagebyte, string date)
                     /
                }
            }*/
        }

        public String AddBookList(string id, int tag, string title, string description, ImageSource image, byte[] imagebyte, string date)
        {
            Models.BookList item = new Models.BookList(id, tag, title, description, image, imagebyte, date);
            DBModels.DBListViewModels.InsertDatabase(item);
            this.allItems.Add(item);
            return item.id;
        }

        public String RemoveBookList(string id)
        {
            String str = this.selectedItem.id;
            this.allItems.Remove(this.selectedItem);
            DBModels.DBListViewModels.DeleteDatabase(str);
            // set selectedItem to null after remove
            this.selectedItem = null;
            return str;
        }

        public String UpdateBookList(string id, int tag, string title, string description, ImageSource image, byte[] imagebyte, string date)
        {
            this.selectedItem.id = id;
            this.selectedItem.tag = tag;
            this.selectedItem.title = title;
            this.selectedItem.description = description;
            this.selectedItem.cover = image;
            this.selectedItem.imagebyte = imagebyte;
            this.selectedItem.date = date;
            DBListViewModels.UpdateDatabase(this.selectedItem);
            // set selectedItem to null after update
            this.selectedItem = null;
            return id;
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

    }
}
