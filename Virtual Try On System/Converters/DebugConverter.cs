using System.Diagnostics;
using System.Windows.Data;

namespace Virtual_Try_On_System.Converters
{
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }
    }
}
