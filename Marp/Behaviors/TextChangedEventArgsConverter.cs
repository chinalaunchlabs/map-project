using System;
using Xamarin.Forms;
using System.Globalization;

namespace Marp.Behaviors
{
	public class TextChangedEventArgsConverter: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
		{
			var eventArgs = value as TextChangedEventArgs;
			if (eventArgs == null)
				throw new ArgumentException ("Expected TextChangedEventArgs as value", "value");
			return eventArgs.NewTextValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

