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

//			var page = FreshPageModelResolver.ResolvePageModel<MapPageModel> ();
//			var basicNavContainer = new FreshNavigationContainer (page);
//			MainPage = basicNavContainer;
//
			// Old UI with master detail page
			var masterDetailNav = new FreshMasterDetailNavigationContainer ();
			masterDetailNav.Init ("Menu");
			masterDetailNav.AddPage<MapPageModel> ("Home");
			masterDetailNav.AddPage<ViewLocationPageModel> ("Saved Locations");

			MainPage = masterDetailNav;
		}

	}
}

