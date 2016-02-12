using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Marp
{
	public class MapView : ContentPage
	{
		public MapView ()
		{
			var map = new Map (MapSpan.FromCenterAndRadius (new Position (37, -122), Distance.FromMiles (0.3))) {
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var stack = new StackLayout { 
				Spacing = 0,
				Children = {
					map
				}
			};
			Content = stack;
			Padding = new Thickness (0, Device.OnPlatform (20, 0, 0), 0, 0);
		}
	}
}

