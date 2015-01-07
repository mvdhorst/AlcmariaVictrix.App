using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlcmariaVictrix.Shared.Converters
{
    public class GameCodeConverter : IValueConverter
    {
        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is int))
                return null;

            var code = (int)value;

            //var weatherCode = WeatherCodes.Instance.FirstOrDefault(x => x.Code == code);

            return null;// weatherCode.Icon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}
