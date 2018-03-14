using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MyTodos.Models
{
    class BookList
    {
        public string id { get; set; }

        public int tag { get; set; } //1——收藏书单 2——已读书单 3——计划书单

        public SymbolIcon tagimage { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string date { get; set; }

        public ImageSource cover { get; set; }

        public List<Book> books { get; set; }

        public byte[] imagebyte { get; set; }

        public BookList(string id, int tag, string title, string description, ImageSource cover, byte[] imagebyte, string date)
        {
            this.id = id;
            if (this.id == "a")
            {
                this.id = Guid.NewGuid().ToString(); //生成id
            }
            this.tag = tag;
            this.title = title;
            this.date = date;
            this.description = description;
            this.cover = cover;
            this.imagebyte = imagebyte;
            if (tag == 1)
            {
                tagimage = new SymbolIcon(Symbol.OutlineStar);
            } else if(tag == 2)
            {
                tagimage = new SymbolIcon(Symbol.Bullets);
            } else if (tag == 3)
            {
                tagimage = new SymbolIcon(Symbol.Library);
            }
        }
    }
}
