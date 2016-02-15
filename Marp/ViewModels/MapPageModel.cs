using System;
using Marp.Geocoder;
using FreshMvvm;
using Xamarin.Forms.Maps;
using Marp.Models;

namespace Marp
{
	// TODO: Clean up this really ugly piece of code.
	public class MapPageModel: FreshBasePageModel
	{
		private MyLocation _location;
		private MapPage _mapPage;

		protected override void ViewIsAppearing(object sender, System.EventArgs e) {

			_mapPage = (MapPage)CurrentPage;
			_location = _mapPage.result;

			if (_location != null) {
				var locations = App.LocationsInSession;
				Position p = _location.Position;

				_mapPage.map.MoveToRegion (new MapSpan (p, 0.5, 0.5));
				_mapPage.map.Pins.Add (new Pin() {
					Label = _location.Address,
					Position = _location.Position
				});
				foreach (var location in locations) {
					_mapPage.map.Pins.Add (new Pin () {
						Label = location.Address,
						Position = location.Position
					});
				}
			}

			base.ViewIsAppearing (sender, e);
		}



	}
}

