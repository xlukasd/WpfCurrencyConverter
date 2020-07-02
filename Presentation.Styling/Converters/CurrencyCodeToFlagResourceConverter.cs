using System;
using System.Globalization;
using System.Windows.Data;

namespace Presentation.Styling.Converters
{
    internal class CurrencyCodeToFlagResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"Flags\\{value.ToString().ToLowerInvariant()}.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
