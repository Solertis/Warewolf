/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2017 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Globalization;
using System.Windows.Data;


namespace Dev2.Studio.Core.AppResources.Converters
{
    public class IntEnsureMinConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert from view model int property to text

            return value; // nothing to be done - this convert is about ensuring valid min input - see ConvertBack
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert from user-captured text to view model int property

            var minValue = GetInt(parameter);
            var intValue = GetInt(value);

            return intValue >= minValue ? intValue : minValue;
        }

        static int GetInt(object value)
        {
            if(value != null)
            {
                if (int.TryParse(value.ToString(), out int intVal))
                {
                    return intVal;
                }
            }
            return 0;
        }
    }
}
