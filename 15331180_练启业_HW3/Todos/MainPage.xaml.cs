using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Todos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
            this.ViewModel = new ViewModels.TodoItemViewModel();
        }

        ViewModels.TodoItemViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.GetType() == typeof(ViewModels.TodoItemViewModel))
            {
                this.ViewModel = (ViewModels.TodoItemViewModel)(e.Parameter);
            }
        }

        private void TodoItem_ItemClicked(object sender, ItemClickEventArgs e)
        {
            ViewModel.SelectedItem = (Models.TodoItem)(e.ClickedItem);
            if (Window.Current.Bounds.Width < 800)
            {
                Frame.Navigate(typeof(NewPage), ViewModel);
            }
            else
            {
                Title.Text = ViewModel.SelectedItem.title;
                Details.Text = ViewModel.SelectedItem.description;
            }
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if(Window.Current.Bounds.Width < 800)
                Frame.Navigate(typeof(NewPage), ViewModel);
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (Title.Text.Trim() == String.Empty)
            {
                var i = new MessageDialog("标题不能为空").ShowAsync();
                flag = false;
            }
            if (Details.Text.Trim() == String.Empty)
            {
                var i = new MessageDialog("细节不能为空").ShowAsync();
                flag = false;
            }
            if (MyDate.Date > DateTimeOffset.Now)
            {
                var i = new MessageDialog("无效日期").ShowAsync();
                flag = false;
            }
            if (flag)
            {
                var i = new MessageDialog("Created").ShowAsync();
                ViewModel.AddTodoItem(Title.Text, Details.Text);
                Title.Text = ""; 
                Details.Text = "";
            }

        }

    }
}
