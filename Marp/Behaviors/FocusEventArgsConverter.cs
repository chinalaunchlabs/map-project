using System;
using Xamarin.Forms;
using System.Globalization;

namespace Marp.Behaviors
{
	public class FocusEventArgsConverter: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
		{
			var eventArgs = value as FocusEventArgs;
			if (eventArgs == null)
				throw new ArgumentException ("Expected TextChangedEventArgs as value", "value");
			return eventArgs.IsFocused;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

