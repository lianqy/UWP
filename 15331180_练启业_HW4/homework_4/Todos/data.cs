﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Todos
{
    public class data : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool? ischeched = value as bool?;
            if (ischeched == null || ischeched == false)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}