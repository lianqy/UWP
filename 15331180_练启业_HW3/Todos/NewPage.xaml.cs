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
                    string title_text = title.Text;
                    string detail_text = details.Text;
                    ViewModel.AddTodoItem(title_text, detail_text);
                    title.Text = "";
                    details.Text = "";
                    Frame.Navigate(typeof(MainPage), ViewModel);
                }
            }
            else
            {
                ViewModel.SelectedItem.rewrite_title(title.Text);
                ViewModel.SelectedItem.rewrite_details(details.Text);
                Frame.Navigate(typeof(MainPage), ViewModel);
            }
                
        }

        private  void DeleteButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                ViewModel.RemoveTodoItem(ViewModel.SelectedItem.get_id());
                Frame.Navigate(typeof(MainPage), "");
            }
        }


        private void UpdateButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                // check then update
              
            }
        }
    }
}
