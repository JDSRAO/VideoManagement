using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using VideoManagement.Models.Tables;

namespace VideoManagement.UI.WPF.Convertors
{
    public class FileSizeToTextConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var file = (AppFile)value;
            var currentSize = file.Size;
            var size = GetFileMetricString(currentSize);
            return $"{size} MB";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetFileMetricString(double size)
        {
            var metricString = string.Empty;
            var inputString = size.ToString();
            var pattern = @"{\w+.\w+}";
            Regex regex = new Regex(pattern);
            var textParts = regex.Split(inputString);
            var decimalPart = textParts[0];
            var strBuilder = new StringBuilder(decimalPart);
            int count = 1;
            for (int i = strBuilder.Length - 1; i == 0; i--)
            {
                if (count % 3 == 0)
                {
                    strBuilder.Insert(i-1, ",", 1);
                }
                count++;
            }

            if(textParts.Length > 1)
            {
                metricString = $"{decimalPart}.{textParts[1]}";
            }
            else
            {
                metricString = $"{decimalPart}";
            }
            return metricString;
        }
    }
}
