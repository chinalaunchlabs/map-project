using System;
using Marp.Geocoder;
using Xamarin.Forms;
using System.Windows.Input;
using FreshMvvm;

namespace Marp
{
	public class LocationCellViewModel: FreshBasePageModel
	{
		private Result _result;
		public LocationCellViewModel (Result r)
		{
			_result = r;
		}

		public string Address {
			get { return _result.formatted_address; }
		}
		public Location Position {
			get { return _result.geometry.location; }
		}

		public ICommand GoToLocCommand {
			get {
				return new Command (async () => {
					App.LocationsInSession.Add(_result);
					MessagingCenter.Send <LocationCellViewModel, Result>(this, "CellTapped", _result);
				});
			}
		}
	}
}

