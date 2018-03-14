using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTodos.Models;
using Booklists;

namespace MyTodos.DBModels
{
    class DBViewModels
    {        /*
         * id string PRIMARY KEY NOT NULL,
                            fid string,
                            title string,
                            description string,
                            classification string,
                            introduction string,
                            buy_link string,
                            completed string,
                            cover string,
                            imagebyte string,
                            date string
        */
        public static void InsertDatabase(Book item)
        {
            var db = App.conn2;
            using (var mydb = db.Prepare("INSERT INTO item2(id, fid, title, description, classification, introduction, buy_link, completed, imagebyte, date) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"))
            {
                mydb.Bind(1, item.id);
                mydb.Bind(2, item.fid);
                mydb.Bind(3, item.title);
                mydb.Bind(4, item.description);
                mydb.Bind(5, item.classification);
                mydb.Bind(6, item.introduction);
                mydb.Bind(7, item.buy_link);
                mydb.Bind(8, item.completed.ToString());
                mydb.Bind(9, item.imagebyte);  //TODO
                mydb.Bind(10, item.date);
                mydb.Step();
            }
        }
        public static void DeleteDatabase(string id)
        {
            var db = App.conn2;
            using (var mydb = db.Prepare("DELETE FROM item2 WHERE ID = ?"))
            {
                mydb.Bind(1, id);
                mydb.Step();
            }
        }
        public static void UpdateDatabase(Book item)
        {
            var db = App.conn2;
            using (var mydb = db.Prepare("UPDATE item2 SET fid = ?, title = ?, description = ?, classification = ?, introduction = ?, buy_link = ?, completed = ?, imagebyte = ?, date = ? WHERE id = ?")) ////
            {
                mydb.Bind(1, item.fid);
                mydb.Bind(2, item.title);
                mydb.Bind(3, item.description);
                mydb.Bind(4, item.classification);
                mydb.Bind(5, item.introduction);
                mydb.Bind(6, item.buy_link);
                mydb.Bind(7, item.completed.ToString());
                mydb.Bind(8, item.imagebyte);  //TODO
                mydb.Bind(9, item.date);
                mydb.Bind(10, item.id);
                mydb.Step();
            }
        }
        /*
        public static BookList GetItem(string w)
        {
            StringBuilder sb;
            sb = new StringBuilder();
            var db = App.conn2;
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
