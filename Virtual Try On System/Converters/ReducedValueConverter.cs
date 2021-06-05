using System;
using System.Windows.Data;
namespace Virtual_Try_On_System.Converters
{
    public class ReducedValueConverter : IValueConverter
    {
       
        // A converted value. If the method returns null, the valid null value is used.
       
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (double.Parse(value.ToString()) - double.Parse(parameter.ToString()));
        }
      
        // A converted value. If the method returns null, the valid null value is used.
       
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
