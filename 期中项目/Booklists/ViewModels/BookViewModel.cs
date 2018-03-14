using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyTodos.Models;
using Windows.UI.Xaml.Media;

namespace MyTodos.ViewModels
{
    class BookViewModel
    {
        private string fid;
        private ObservableCollection<Models.Book> allItems = new ObservableCollection<Models.Book>();
        public ObservableCollection<Models.Book> AllItems { get { return this.allItems; } }

        private Models.Book selectedItem = default(Models.Book);
        public Models.Book SelectedItem { get { return selectedItem; } set { this.selectedItem = value; } }

        public BookViewModel()
        {
            // 加入两个用来测试的item
            //this.allItems.Add(new Models.Book("发条橙", "窥恶癖", DateTime.Now.ToString(), "小说", "小混混的成长历程", "www.taobao.xxxx"));
            //this.allItems.Add(new Models.Book("白夜行", "东野圭吾", DateTime.Now.ToString(), "小说", "日本亚马逊销量第一", "www.taobao.xxx"));
            /*string q = "%%"; //意为查询所有的数据
            using (var statement = App.conn.Prepare("SELECT Id, Title, Details, Date FROM Customer WHERE Id LIKE ? OR Title LIKE ? OR Details LIKE ? OR Date LIKE ?"))
            {
                statement.Bind(1, q);
                statement.Bind(2, q);
                statement.Bind(3, q);
                statement.Bind(4, q);
                while (statement.Step() != SQLiteResult.DONE)
                {
                    string did = statement[0].ToString();
                    string dtitle = statement[1].ToString();
                    string ddetails = statement[2].ToString();
                    string ddate = statement[3].ToString();
                    DateTime a = Convert.ToDateTime(ddate);
                    this.allItems.Add(new Models.TodoItem(did, dtitle, ddetails, a));
                }
            }*/
        }

        public String AddBook(bool ok, string id, string fid, string title, string description, string classification, string introduction, string buy_link, bool completed, ImageSource cover, byte[] imagebyte, string date)
        {
            Models.Book item = new Models.Book(id, fid, title, description, classification, introduction, buy_link, completed, cover, imagebyte, date); ///TODO
            this.allItems.Add(item);
            if(ok == false) DBModels.DBViewModels.InsertDatabase(item);
            this.selectedItem = null;
            return item.id;
        }
        public void setfid(string fid)
        {
            this.fid = fid;
        }
        public string getfid()
        {
            return fid;
        }
        public void addbook(Book obj)
        {
            Models.Book item = new Book(obj);
            this.allItems.Add(item);
            DBModels.DBViewModels.InsertDatabase(item);
            this.selectedItem = null;
        }
        public String RemoveBook(string id)
        {
            String str = this.selectedItem.id;
            this.allItems.Remove(this.selectedItem);
            // set selectedItem to null after remove
            DBModels.DBViewModels.DeleteDatabase(str);
            this.selectedItem = null;
            return str;
        }

        public void UpdateBook(string title, string description, string classification, string introduction, string buy_link, bool? completed, ImageSource cover, byte[] imagebyte, string date)
        {
            this.selectedItem.title = title;
            this.selectedItem.description = description;
            this.selectedItem.classification = classification;
            this.selectedItem.introduction = introduction;
            this.selectedItem.buy_link = buy_link;
            this.selectedItem.completed = completed;
            this.selectedItem.cover = cover;
            this.selectedItem.imagebyte = imagebyte;
            this.selectedItem.date = date;
            DBModels.DBViewModels.UpdateDatabase(this.selectedItem);
            // set selectedItem to null after update
            this.selectedItem = null;
        }

    }
}
