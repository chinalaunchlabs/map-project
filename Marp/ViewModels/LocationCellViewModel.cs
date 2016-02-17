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
			get { return _location.Address.Truncate(40); }
		}


		public float FaveOpacity {
			get {
				return 1;
			}
		}

		public ICommand GoToLocCommand {
			get {
				return new Command (async (gdf) => {
					MessagingCenter.Send <LocationCellViewModel, MyLocation>(this, "CellTapped", _location);
				});
			}
		}

		public ICommand SaveLocationCommand {
			get { 
				return new Command (() => {
					App.Database.SaveLocation(_location);
				});
			}
		}
	}
}

