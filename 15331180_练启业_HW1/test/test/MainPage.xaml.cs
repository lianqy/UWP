using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace test
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private delegate string AnimalSaying(object sender, myEventArgs e);//声明一个委托
        private event AnimalSaying Say;//委托声明一个事件
        private int times = 0;

        public MainPage()
        {
            this.InitializeComponent();
        }

        interface test
        {
            string saying(object sender, myEventArgs e);
            int A { get; set; }
        }

        class cat : test
        {
            TextBlock word;
            private int a;

            public cat(TextBlock w)
            {
                this.word = w;
            }
            public string saying(object sender, myEventArgs e)
            {
                this.word.Text += "Cat:I am a cat.\n";
                return "";
            }
            public int A
            {
                get { return a; }
                set { this.a = value; }
            }
        }

        class dog : test
        {
            TextBlock word;
            private int a;

            public dog(TextBlock w)
            {
                this.word = w;
            }
            public string saying(object sender, myEventArgs e)
            {
                this.word.Text += "Dog:I am a dog.\n";
                return "";
            }
            public int A
            {
                get { return a; }
                set { this.a = value; }
            }
        }

        class pig : test
        {
            TextBlock word;
            private int a;

            public pig(TextBlock w)
            {
                this.word = w;
            }
            public string saying(object sender, myEventArgs e)
            {
                this.word.Text += "Pig:I am a pig.\n";
                return "";
            }
            public int A
            {
                get { return a; }
                set { this.a = value; }
            }
        }

        private cat c;
        private dog d;
        private pig p;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            word_out.Text = "";
            Random ran = new Random();
            int key = ran.Next(1,4);
            if (key == 1)
            {
                c = new cat(word_out);
                Say += new AnimalSaying(c.saying);
            }
            else if (key == 2)
            {
                d = new dog(word_out);
                Say += new AnimalSaying(d.saying);
            }
            else if (key == 3)
           {
                p = new pig(word_out);
                Say += new AnimalSaying(p.saying);
            }
            
            Say(this, new myEventArgs(times++));  //事件中传递参数times
        }

        //自定义一个Eventargs传递事件参数
        class myEventArgs : EventArgs
        {
            public int t = 0;
            public myEventArgs(int tt)
            {
                this.t = tt;
            }
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (word_in.Text == "dog")
            {
                word_out.Text = "";
                d = new dog(word_out);
                Say += new AnimalSaying(d.saying);
                Say(this, new myEventArgs(times++));  //事件中传递参数times
            }
            else if (word_in.Text == "cat")
            {
                word_out.Text = "";
                c = new cat(word_out);
                Say += new AnimalSaying(c.saying);
                Say(this, new myEventArgs(times++));  //事件中传递参数times
            }
            else if (word_in.Text == "pig")
            {
                word_out.Text = "";
                p = new pig(word_out);
                Say += new AnimalSaying(p.saying);
                Say(this, new myEventArgs(times++));  //事件中传递参数times
            }
            word_in.Text = "";
        }
    }
}
