using System.Globalization;
using System.Windows.Data;

namespace KabeGami.Desktop.Common.Converters;
internal sealed class DictionaryValueConverter
    : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is Guid guid && values[1] is IDictionary<Guid, string> dictionary)
        {
            if (dictionary.TryGetValue(guid, out var result))
            {
                return result;
            }
        }
        return null!;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

