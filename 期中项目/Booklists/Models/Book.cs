using System;
using Windows.UI.Xaml.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTodos.Models
{
    class Book
    {
        public string id { get; set; }

        public string fid { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string classification { get; set; }

        public string introduction { get; set; }

        public string buy_link { get; set; }

        public bool? completed { get; set; }
        
        public ImageSource cover { get; set; }

        public byte[] imagebyte { get; set; }

        public string date { get; set; }

        public Book(string id, string fid, string title, string description, string classification, string introduction, string buy_link, bool completed, ImageSource cover, byte[] imagebyte, string date)
        {
            this.id = id;
            if(id == "a")
            {
                this.id = Guid.NewGuid().ToString(); //生成id
            }
            this.fid = fid;
            this.title = title;
            this.description = description;
            this.completed = completed;
            this.classification = classification;
            this.introduction = introduction;
            this.buy_link = buy_link;
            this.cover = cover;
            this.imagebyte = imagebyte;
            this.date = date;
        }

        public Book(Book obj)
        {
            this.id = obj.id;
            this.fid = obj.fid;
            this.title = obj.title;
            this.description = obj.description;
            this.date = obj.date;
            this.classification = obj.classification;
            this.introduction = obj.introduction;
            this.buy_link = obj.buy_link;
            this.completed = obj.completed;
            this.cover = obj.cover;
            this.imagebyte = obj.imagebyte;
        }
    }
}
