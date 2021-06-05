using System;
using System.Windows.Data;

namespace Virtual_Try_On_System.Converters
{
    class IncreasedValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double number;
            double.TryParse((string)parameter, out number);

            return (double.Parse(value.ToString()) + number);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
