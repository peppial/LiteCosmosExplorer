using Avalonia.Data.Converters;

namespace CosmosExplorer.Avalonia.Behaviours;

using System;
using System.Globalization;

public class DivideByTenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal decimalValue || value is int intValue)
        {
            decimal result = (value is decimal ? (decimal)value : (int)value) * 100.0m;
            string format = parameter as string ?? "N0";
            return string.Format(culture, "{0:N0}%", result);
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}