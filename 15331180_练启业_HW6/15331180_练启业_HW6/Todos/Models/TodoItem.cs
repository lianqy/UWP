using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todos.Models
{
    public class TodoItem
    {

        private long id;

        public string title { get; set; }

        public string description { get; set; }

        public bool completed { get; set; }

        public DateTime date { get; set; }

        public TodoItem(long id, string title, string description, DateTime date)
        {
            this.id = id;
            this.title = title;
            this.date = date;
            this.description = description;
            this.completed = false; //默认为未完成
        }

        public long get_id()
        {
            return this.id;
        }

        public void rewrite_title(string title)
        {
            this.title = title;
        }

        public void rewrite_details(string detail)
        {
            this.description = detail;
        }

        public void rewrite_date(DateTime date)
        {
            this.date = date;
        }
    }
}
