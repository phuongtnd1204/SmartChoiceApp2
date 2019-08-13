using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartChoiceApp.Converter
{
    public class ToDouble : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double.Parse(value.ToString()) *2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)(int.Parse(value.ToString()) / 2);
        }
    }
}
