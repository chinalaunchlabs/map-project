using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Marp.Geocoder;
using Marp.Models;

namespace Marp
{
	public partial class MapPage : ContentPage
	{
		public Map map;
		public MyLocation result;
		public MapPage ()
		{
			InitializeComponent ();
//			map = _map;
//			map = new Map () {
//				IsShowingUser = true,
//				HeightRequest = 100,
//				WidthRequest = 960,
//				VerticalOptions = LayoutOptions.FillAndExpand
//			};
//
//			var stack = new StackLayout { 
//				Spacing = 0,
//				Children = {
//					map
//				}
//			};
//			Content = stack;
//			Padding = new Thickness (0, Device.OnPlatform (20, 0, 0), 0, 0);
		}
	}
}

