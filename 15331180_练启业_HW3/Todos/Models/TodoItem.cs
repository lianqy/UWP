using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todos.Models
{
    class TodoItem
    {

        private string id;

        public string title { get; set; }

        public string description { get; set; }

        public bool completed { get; set; }

        //日期字段自己写

        public TodoItem(string title, string description)
        {
            this.id = Guid.NewGuid().ToString(); //生成id
            this.title = title;
            this.description = description;
            this.completed = false; //默认为未完成
        }

        public string get_id()
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
    }
}
