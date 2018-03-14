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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Diagnostics;

namespace Todos
{
    public sealed partial class NewPage : Page
    {
        public NewPage()
        {
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;

        }

        private ViewModels.TodoItemViewModel ViewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = ((ViewModels.TodoItemViewModel)e.Parameter);
            if (ViewModel.SelectedItem == null)
            {
                createButton.Content = "Create";
                //var i = new MessageDialog("Welcome!").ShowAsync();
            }
            else
            {
                createButton.Content = "Update";
                title.Text = ViewModel.SelectedItem.title;
                details.Text = ViewModel.SelectedItem.description;
            }
        }
        private async void CreateButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem == null)
            {
                if (title.Text == "" && details.Text == "")
                {
                    MessageDialog msg = new MessageDialog("标题和细节不能为空!");
                    var msginfo = await msg.ShowAsync();
                }
                else if (title.Text == "" && details.Text != "")
                {
                    MessageDialog msg = new MessageDialog("标题不能为空!");
                    var msginfo = await msg.ShowAsync();
                }
                else if (title.Text != "" && details.Text == "")
                {
                    MessageDialog msg = new MessageDialog("细节不能为空!");
                    var msginfo = await msg.ShowAsync();
                }
                else
                {
                    //string title_text = title.Text;
                   // string detail_text = details.Text;

                    var db = App.conn;
                    string sql = @"INSERT INTO Todo (date, title, detail, finish) VALUES (?,?,?,?)";
                    using (var res = db.Prepare(sql))
                    {
                        res.Bind(1, MyDate.Date.DateTime.ToString());
                        res.Bind(2, title.Text.Trim());
                        res.Bind(3, details.Text.Trim());
                        res.Bind(4, "false");
                        res.Step();
                    }

                    ViewModel.AddTodoItem(db.LastInsertRowId(), title.Text, details.Text, MyDate.Date.DateTime);
                    title.Text = "";
                    details.Text = "";
                    Frame.Navigate(typeof(MainPage), ViewModel);
                }
            }
            else
            {

                var db = App.conn;
                string sql = @"UPDATE Todo SET date = ?, title = ?, detail = ? WHERE ID = ?";
                using (var res = db.Prepare(sql))
                {
                    res.Bind(1, MyDate.Date.DateTime.ToString());
                    res.Bind(2, title.Text.Trim());
                    res.Bind(3, details.Text.Trim());
                    res.Bind(4, ViewModel.SelectedItem.get_id());
                    res.Step();
                }

                ViewModel.SelectedItem.rewrite_title(title.Text);
                ViewModel.SelectedItem.rewrite_details(details.Text);
                ViewModel.SelectedItem.rewrite_date(MyDate.Date.DateTime);
                Frame.Navigate(typeof(MainPage), ViewModel);
            }
                
        }

        private  void DeleteButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                var db = App.conn;
                string sql = @"DELETE FROM Todo WHERE ID = ?";
                try
                {
                    using (var res = db.Prepare(sql))
                    {
                        res.Bind(1, ViewModel.SelectedItem.get_id());
                        res.Step();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }

                ViewModel.RemoveTodoItem(ViewModel.SelectedItem.get_id());
                Frame.Navigate(typeof(MainPage), ViewModel);
            }
        }


        private void UpdateButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                // check then update
              
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), ViewModel);
        }
    }
}
