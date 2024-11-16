using System.Globalization;
using System.Windows.Data;

namespace KabeGami.Desktop.Common.Converters;
internal sealed class GuidMatchToBooleanConverter
    : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is Guid guid1 && values[1] is Guid guid2)
        {
            bool shouldInvert = parameter as string == "Invert";
            var isEqual = guid1 == guid2;
            return shouldInvert ? !isEqual : isEqual;
        }
        return null!;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return [];
    }
}
