using System;
using Marp.Geocoder;
using FreshMvvm;
using Xamarin.Forms.Maps;

namespace Marp
{
	// TODO: Clean up this really ugly piece of code.
	public class MapPageModel: FreshBasePageModel
	{
		private Result _result;
		private MapPage _mapPage;

		protected override void ViewIsAppearing(object sender, System.EventArgs e) {

			_mapPage = (MapPage)CurrentPage;
			_result = _mapPage.result;

			var locations = App.LocationsInSession;
			Position p = new Position (_result.geometry.location.lat, _result.geometry.location.lng);
			System.Diagnostics.Debug.WriteLine (p.ToString());

			_mapPage.map.MoveToRegion (new MapSpan (p, 0.5, 0.5));
			foreach (var location in locations) {
				p = new Position (location.geometry.location.lat, location.geometry.location.lng);
				_mapPage.map.Pins.Add (new Pin () {
					Label = location.formatted_address,
					Position = p
				});
			}

			base.ViewIsAppearing (sender, e);
		}



	}
}

