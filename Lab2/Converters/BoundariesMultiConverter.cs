using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpringLab2.Converters
{
    public class BoundariesMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string boundaries;
            boundaries = values[0] + ";" + values[1];
            return boundaries;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] splitValues = ((string)value).Split(';');
            double doubleA=0.0;
            double doubleB=0.0;
            if (splitValues.Length == 2)
            {
                double.TryParse(splitValues[0], out doubleA);
                double.TryParse(splitValues[1], out doubleB);
            }
            object[] doubles =  { doubleA, doubleB };
            return doubles;
        }
    }
}
