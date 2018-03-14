using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTodos.Models;
using Booklists;
using Windows.Storage;
using Windows.Storage.Streams;

namespace MyTodos.DBModels
{
    class DBListViewModels
    {
        public static void InsertDatabase(BookList item)
        {
            var db = App.conn;
            using (var mydb = db.Prepare("INSERT INTO item(id, tag, title, description, imagebyte, date) VALUES(?, ?, ?, ?, ?, ?)"))
            {
                mydb.Bind(1, item.id);
                mydb.Bind(2, item.tag);
                mydb.Bind(3, item.title);
                mydb.Bind(4, item.description);
                mydb.Bind(5, item.imagebyte);   /////TODO
                mydb.Bind(6, item.date);
                mydb.Step();
            }
        }
        public static void DeleteDatabase(string id)
        {
            var db = App.conn;
            using (var mydb = db.Prepare("DELETE FROM item WHERE ID = ?"))
            {
                mydb.Bind(1, id);
                mydb.Step();
            }
        }
        public static void UpdateDatabase(BookList item)
        {
            var db = App.conn;
            using (var mydb = db.Prepare("UPDATE item SET tag = ?, title = ?, description = ?, imagebyte = ?, date = ? WHERE id = ?"))
            {
                mydb.Bind(1, item.tag); //
                mydb.Bind(2, item.title);
                mydb.Bind(3, item.description);
                mydb.Bind(4, item.imagebyte);
                mydb.Bind(5, item.date);
                mydb.Bind(6, item.id);
                mydb.Step();
            }
        }
        /*
        public static BookList GetItem(string w)
        {
            StringBuilder sb;
            sb = new StringBuilder();
            var db = App.conn;
            BookList todoItem = null;
            using (var mydb = db.Prepare("SELECT id, title, description, completed, date FROM item WHERE title LIKE '%'||?||'%' OR description LIKE '%'||?||'%'"))
            {
                mydb.Bind(1, w);
                mydb.Bind(2, w);
                while (SQLiteResult.ROW == mydb.Step())
                {
                    sb.Append("title = ").Append(mydb[1].ToString()).Append(", description = ").Append(mydb[2].ToString()).Append(", date = ").Append(DateTimeOffset.Parse(mydb[4].ToString())).Append("\n");
                    todoItem = new TodoItem((string)mydb[0], (string)mydb[1], (string)mydb[2], DateTimeOffset.Parse((string)mydb[4]), false);
                }
            }
            var i = new Windows.UI.Popups.MessageDialog(sb.ToString()).ShowAsync();
            return todoItem;
        }
        */

    }
}
