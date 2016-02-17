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
			_inDatabase = App.Database.InDatabase (_location.Address);
		}

		public string Address {
			get { return _location.Address.Truncate(40); }
		}

		private bool _inDatabase;
		public bool InDatabase {
			get {
				return _inDatabase;
			}
			set {
				if (value)
					App.Database.SaveLocation (_location);
				else
					App.Database.DeleteLocation (_location.ID);
				_inDatabase = value;
			}
		}

		public float FaveOpacity {
			get {
				if (InDatabase)
					return 1;
				else
					return 0.1f;
			}
		}

		public ICommand GoToLocCommand {
			get {
				return new Command ((gdf) => {
					MessagingCenter.Send <LocationCellViewModel, MyLocation>(this, "LocationTapped", _location);
				});
			}
		}

		public ICommand SaveLocationCommand {
			get { 
				return new Command (() => {
					MessagingCenter.Send <LocationCellViewModel, MyLocation>(this, "StarTapped", _location);
					InDatabase = !InDatabase;	
					RaisePropertyChanged("FaveOpacity");
				});
			}
		}
	}
}

