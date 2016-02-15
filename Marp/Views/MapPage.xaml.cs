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
			map = new Map () {
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

//		public void MoveTo(Position p) {
//			System.Diagnostics.Debug.WriteLine ("MapPage::Moving to {0}, {1}", p.Latitude, p.Longitude);
//			map.MoveToRegion (new MapSpan (p, 0.1, 0.1));
//		}
//
//		public void AddPins() {
//			foreach (var location in App.LocationsInSession) {
//				var latlng = location.geometry.location;
//				map.Pins.Add (
//					new Pin() {
//						Position = new Position(latlng.lat, latlng.lng),
//						Label = location.formatted_address
//					}
//				);
//			}
//		}
	}
}

