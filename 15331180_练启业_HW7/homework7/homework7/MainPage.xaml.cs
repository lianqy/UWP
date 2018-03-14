using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace homework7
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        async void queryAsync(string cityname)
        {
            string appkey = "92024f0aaf1b4d0f3429d477eb3b9eb7";
            string url = "http://v.juhe.cn/weather/index?format=2&cityname=" + cityname + "&key=" + appkey;
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync(url);
            JsonReader reader = new JsonTextReader(new StringReader(result));
            while (reader.Read())
            {
                if ((String)reader.Value == "")
                    throw new Exception();
                if ((String)reader.Value == "weather")
                {
                    reader.Read();
                    textBlock2.Text = (String)reader.Value;
                    break;
                }
                if ((String)reader.Value == "temperature")
                {
                    reader.Read();
                    textBlock4.Text = (String)reader.Value;
                }
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string city = textBox.Text;
            textBox.Text = "";
            queryAsync(city);
        }
    }
}
