using System;

using Xamarin.Forms;
using FreshMvvm;
using Marp.Geocoder;
using System.Collections.Generic;
using Marp.Models;

namespace Marp
{
	public class App : Application
	{
		static GoogleGeocoder _client;
		public static GoogleGeocoder GeocoderClient {
			get {
				_client = _client ?? new GoogleGeocoder ();
				return _client;
			}
		}

		static List<MyLocation> _locationsInSession;
		public static List<MyLocation> LocationsInSession {
			get {
				_locationsInSession = _locationsInSession ?? new List<MyLocation> ();
				return _locationsInSession;
			}
		}

		static LocationDatabase _database;
		public static LocationDatabase Database {
			get {
				_database = _database ?? new LocationDatabase ();
				return _database;
			}
		}

		public App ()
		{

			var masterDetailNav = new FreshMasterDetailNavigationContainer ();
			masterDetailNav.Init ("Menu");
			masterDetailNav.AddPage<MapPageModel> ("Home");
			masterDetailNav.AddPage<AddLocationPageModel> ("Add Location");
			masterDetailNav.AddPage<ViewLocationPageModel> ("View Location");

			MainPage = masterDetailNav;
		}

	}
}

