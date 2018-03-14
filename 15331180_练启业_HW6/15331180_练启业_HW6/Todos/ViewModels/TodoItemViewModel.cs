using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todos.ViewModels
{
    class TodoItemViewModel
    {
        public ObservableCollection<Models.TodoItem> allItems = new ObservableCollection<Models.TodoItem>();
        public ObservableCollection<Models.TodoItem> AllItems { get { return this.allItems; } }

        private Models.TodoItem selectedItem;
        public Models.TodoItem SelectedItem { get { return selectedItem; } set { this.selectedItem = value; }  }

        int count = 2;

        public TodoItemViewModel()
        {
            try
            {
                var sql = "SELECT ID, date, title, detail FROM Todo";
                var db = App.conn;
                using (var statement = db.Prepare(sql))
                {
                    while (SQLiteResult.ROW == statement.Step())
                    {
                        var s = statement[1].ToString();
                        s = s.Substring(0, s.IndexOf(' '));
                        long ID = (long)statement[0];
                        DateTime date = new DateTime(int.Parse(s.Split('/')[0]), int.Parse(s.Split('/')[1]), int.Parse(s.Split('/')[2]));
                        string title = (string)statement[2];
                        string detail = (string)statement[3];
                        this.AddTodoItem(ID, title, detail, date);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddTodoItem(long id, string title, string description, DateTime date)
        {
            this.allItems.Add(new Models.TodoItem(id,title, description, date));
            count++;
        }

        public void RemoveTodoItem(long id)
        {
            int i = 0;
            for (i = 0; i < count; i ++)
            {
                if (allItems[i].get_id() == id)
                    break;
            }
            this.allItems.Remove(allItems[i]);
            this.selectedItem = null;
        }

        public void UpdateTodoItem(string id, string title, string description)
        {
            // DIY

            // set selectedItem to null after update
            this.selectedItem = null;
        }

    }
}
