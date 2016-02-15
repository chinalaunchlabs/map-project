using System;
using Marp.Geocoder;
using Marp.Models;
using Xamarin.Forms;
using System.Windows.Input;
using FreshMvvm;

namespace Marp
{
	public class LocationCellViewModel: FreshBasePageModel
	{
		private MyLocation _location;
		public LocationCellViewModel (MyLocation loc)
		{
			_location = loc;
		}

		public string Address {
			get { return _location.Address; }
		}

		public ICommand GoToLocCommand {
			get {
				return new Command (async (gdf) => {
					App.LocationsInSession.Add(_location);
					App.Database.SaveLocation(_location);
					MessagingCenter.Send <LocationCellViewModel, MyLocation>(this, "CellTapped", _location);
				});
			}
		}

		public ICommand EraseGoToLocCommand {
			get {
				return new Command (async (gdf) => {
//					App.LocationsInSession.Add(_location);
					App.LocationsInSession.Clear();
//					App.Database.SaveLocation(_location);
					MessagingCenter.Send <LocationCellViewModel, MyLocation>(this, "CellTapped2", _location);
				});
			}
		}
	}
}

