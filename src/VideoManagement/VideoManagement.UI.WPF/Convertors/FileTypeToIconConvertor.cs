using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using VideoManagement.Models.Tables;

namespace VideoManagement.UI.WPF.Convertors
{
    public class FileTypeToIconConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var file = (AppFile) value;
            var currentValue = file.Type;
            BitmapImage icon = null;
            switch (currentValue)
            {
                case FileType.Audio:
                    icon = Application.Current.FindResource("musicIcon") as BitmapImage;
                    break;
                case FileType.Video:
                    icon = Application.Current.FindResource("videoIcon") as BitmapImage;
                    break;
                default:
                    icon = Application.Current.FindResource("unknownIcon") as BitmapImage;
                    break;
            }

            return icon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
