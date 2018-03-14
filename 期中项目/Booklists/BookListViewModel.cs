using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
            // 加入三个用来测试的item
            this.allItems.Add(new Models.BookList(1, "春风拂面", "阳光很美好，紫罗兰很美好，爱情很美好"));
            this.allItems.Add(new Models.BookList(2, "大二下", "要加油！"));
            this.allItems.Add(new Models.BookList(3, "30天提高写作技巧", "朋友推荐"));
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

        public String AddBookList(int tag, string title, string description)
        {
            Models.BookList item = new Models.BookList(tag, title, description);
            this.allItems.Add(item);
            return item.id;
        }

        public String RemoveBookList(string id)
        {
            String str = this.selectedItem.id;
            this.allItems.Remove(this.selectedItem);
            // set selectedItem to null after remove
            this.selectedItem = null;
            return str;
        }

        public String UpdateBookList(string id, int tag, string title, string description)
        {
            this.selectedItem.id = id;
            this.selectedItem.tag = tag;
            this.selectedItem.title = title;
            this.selectedItem.description = description;
            //this.selectedItem.cover = s;

            // set selectedItem to null after update
            this.selectedItem = null;
            return id;
        }

    }
}
