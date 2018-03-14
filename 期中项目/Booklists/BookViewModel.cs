using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MyTodos.ViewModels
{
    class BookViewModel
    {
        private ObservableCollection<Models.Book> allItems = new ObservableCollection<Models.Book>();
        public ObservableCollection<Models.Book> AllItems { get { return this.allItems; } }

        private Models.Book selectedItem = default(Models.Book);
        public Models.Book SelectedItem { get { return selectedItem; } set { this.selectedItem = value; } }

        public BookViewModel()
        {
            // 加入两个用来测试的item
            this.allItems.Add(new Models.Book("发条橙", "窥恶癖", DateTime.Now, "小说", "小混混的成长历程", "www.taobao.xxxx"));
            this.allItems.Add(new Models.Book("白夜行", "东野圭吾", DateTime.Now, "小说", "日本亚马逊销量第一", "www.taobao.xxx"));
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

        public String AddBook(string title, string description, DateTime d, string c, string i, string b)
        {
            Models.Book item = new Models.Book(title, description, d, c, i, b);
            this.allItems.Add(item);
            return item.id;
        }

        public String RemoveBook(string id)
        {
            String str = this.selectedItem.id;
            this.allItems.Remove(this.selectedItem);
            // set selectedItem to null after remove
            this.selectedItem = null;
            return str;
        }

        public String UpdateBook(string id, string title, string description, DateTime d, string c, string i, string b)
        {
            this.selectedItem.id = id;
            this.selectedItem.title = title;
            this.selectedItem.description = description;
            this.selectedItem.date = d;
            this.selectedItem.classification = c;
            this.selectedItem.introduction = i;
            this.selectedItem.buy_link = b;
            //this.selectedItem.cover = s;

            // set selectedItem to null after update
            this.selectedItem = null;
            return id;
        }

    }
}
