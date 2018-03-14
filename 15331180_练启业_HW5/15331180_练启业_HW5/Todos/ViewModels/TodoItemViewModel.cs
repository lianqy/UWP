using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todos.ViewModels
{
    class TodoItemViewModel
    {
        public ObservableCollection<Models.TodoItem> allItems = new ObservableCollection<Models.TodoItem>();
        public ObservableCollection<Models.TodoItem> AllItems { get { return this.allItems; } }

        private Models.TodoItem selectedItem;
        public Models.TodoItem SelectedItem { get { return selectedItem; } set { this.selectedItem = value; }  }

        int count = 2;

        public TodoItemViewModel()
        {
            
        }

        public void AddTodoItem(string title, string description, DateTime date)
        {
            this.allItems.Add(new Models.TodoItem(title, description, date));
            count++;
        }

        public void RemoveTodoItem(string id)
        {
            int i = 0;
            for (i = 0; i < count; i ++)
            {
                if (allItems[i].get_id() == id)
                    break;
            }
            this.allItems.Remove(allItems[i]);
            this.selectedItem = null;
        }

        public void UpdateTodoItem(string id, string title, string description)
        {
            // DIY

            // set selectedItem to null after update
            this.selectedItem = null;
        }

    }
}
