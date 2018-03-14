﻿#pragma checksum "C:\Users\user\Desktop\project_v5.4\Booklists\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8FC70BE0DE61CA8A786D2BB1628DADE8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Booklists
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_AppBarButton_Icon(global::Windows.UI.Xaml.Controls.AppBarButton obj, global::Windows.UI.Xaml.Controls.IconElement value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::Windows.UI.Xaml.Controls.IconElement) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Controls.IconElement), targetNullValue);
                }
                obj.Icon = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_Image_Source(global::Windows.UI.Xaml.Controls.Image obj, global::Windows.UI.Xaml.Media.ImageSource value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::Windows.UI.Xaml.Media.ImageSource) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Media.ImageSource), targetNullValue);
                }
                obj.Source = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        private class MainPage_obj17_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IMainPage_Bindings
        {
            private global::MyTodos.Models.BookList dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.AppBarButton obj18;
            private global::Windows.UI.Xaml.Controls.Image obj19;
            private global::Windows.UI.Xaml.Controls.TextBlock obj20;
            private global::Windows.UI.Xaml.Controls.TextBlock obj21;

            public MainPage_obj17_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 18:
                        this.obj18 = (global::Windows.UI.Xaml.Controls.AppBarButton)target;
                        break;
                    case 19:
                        this.obj19 = (global::Windows.UI.Xaml.Controls.Image)target;
                        break;
                    case 20:
                        this.obj20 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 21:
                        this.obj21 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 global::MyTodos.Models.BookList data = args.NewValue as global::MyTodos.Models.BookList;
                 if (args.NewValue != null && data == null)
                 {
                    throw new global::System.ArgumentException("Incorrect type passed into template. Based on the x:DataType global::MyTodos.Models.BookList was expected.");
                 }
                 this.SetDataRoot(data);
                 this.Update();
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                switch(args.Phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(args.Item as global::MyTodos.Models.BookList);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            ((global::Windows.UI.Xaml.Controls.UserControl)args.ItemContainer.ContentTemplateRoot).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::MyTodos.Models.BookList) args.Item, 1 << (int)args.Phase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
            }

            // IMainPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            // MainPage_obj17_Bindings

            public void SetDataRoot(global::MyTodos.Models.BookList newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::MyTodos.Models.BookList obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_tagimage(obj.tagimage, phase);
                        this.Update_cover(obj.cover, phase);
                        this.Update_title(obj.title, phase);
                        this.Update_date(obj.date, phase);
                    }
                }
            }
            private void Update_tagimage(global::Windows.UI.Xaml.Controls.SymbolIcon obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_AppBarButton_Icon(this.obj18, obj, null);
                }
            }
            private void Update_cover(global::Windows.UI.Xaml.Media.ImageSource obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_Image_Source(this.obj19, obj, null);
                }
            }
            private void Update_title(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj20, obj, null);
                }
            }
            private void Update_date(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj21, obj, null);
                }
            }
        }

        private class MainPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IMainPage_Bindings
        {
            private global::Booklists.MainPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ListView obj6;

            public MainPage_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 6:
                        this.obj6 = (global::Windows.UI.Xaml.Controls.ListView)target;
                        break;
                    default:
                        break;
                }
            }

            // IMainPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            // MainPage_obj1_Bindings

            public void SetDataRoot(global::Booklists.MainPage newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::Booklists.MainPage obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_ViewModel(obj.ViewModel, phase);
                    }
                }
            }
            private void Update_ViewModel(global::MyTodos.ViewModels.BookListViewModel obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_ViewModel_AllItems(obj.AllItems, phase);
                    }
                }
            }
            private void Update_ViewModel_AllItems(global::System.Collections.ObjectModel.ObservableCollection<global::MyTodos.Models.BookList> obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj6, obj, null);
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2:
                {
                    this.AddAppBarButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 13 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.AddAppBarButton).Click += this.AddAppBarButton_Click;
                    #line default
                }
                break;
            case 3:
                {
                    this.VisualStateGroup = (global::Windows.UI.Xaml.VisualStateGroup)(target);
                }
                break;
            case 4:
                {
                    this.VisualStateMin0 = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 5:
                {
                    this.VisualStateMin800 = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 6:
                {
                    this.ToDoListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 65 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.ToDoListView).ItemClick += this.ToDoListView_ItemClick;
                    #line default
                }
                break;
            case 7:
                {
                    this.InlineToDoItemViewGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 8:
                {
                    this.bgimage = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 9:
                {
                    this.Title = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 10:
                {
                    this.Details = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 11:
                {
                    global::Windows.UI.Xaml.Controls.Button element11 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 131 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element11).Click += this.Button_Click;
                    #line default
                }
                break;
            case 12:
                {
                    this.tag = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 13:
                {
                    this.SelectPictureButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 126 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.SelectPictureButton).Click += this.SelectPictureButton_Click;
                    #line default
                }
                break;
            case 14:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element14 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 120 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element14).Click += this.MenuFlyoutItem_Click_2;
                    #line default
                }
                break;
            case 15:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element15 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 121 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element15).Click += this.MenuFlyoutItem_Click_3;
                    #line default
                }
                break;
            case 16:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element16 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 122 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element16).Click += this.MenuFlyoutItem_Click_4;
                    #line default
                }
                break;
            case 22:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element22 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 100 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element22).Click += this.MenuFlyoutItem_Click_1;
                    #line default
                }
                break;
            case 23:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element23 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 101 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element23).Click += this.MenuFlyoutItem_Click;
                    #line default
                }
                break;
            case 24:
                {
                    this.searchBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 25:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element25 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 57 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element25).Click += this.AppBarButton_Click;
                    #line default
                }
                break;
            case 26:
                {
                    this.textBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    MainPage_obj1_Bindings bindings = new MainPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            case 17:
                {
                    global::Windows.UI.Xaml.Controls.UserControl element17 = (global::Windows.UI.Xaml.Controls.UserControl)target;
                    MainPage_obj17_Bindings bindings = new MainPage_obj17_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot((global::MyTodos.Models.BookList) element17.DataContext);
                    element17.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element17, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

