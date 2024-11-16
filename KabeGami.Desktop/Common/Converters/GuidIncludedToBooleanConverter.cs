using System.Globalization;
using System.Windows.Data;

namespace KabeGami.Desktop.Common.Converters;
internal sealed class GuidIncludedToBooleanConverter
    : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is Guid guid && values[1] is IList<Guid> guids)
        {
            bool shouldInvert = parameter as string == "Invert";
            var isEqual = guids.Contains(guid);
            return shouldInvert ? !isEqual : isEqual;
        }
        return null!;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return [];
    }
}
