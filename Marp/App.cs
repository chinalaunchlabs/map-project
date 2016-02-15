using System;

using Xamarin.Forms;
using FreshMvvm;
using Marp.Geocoder;
using System.Collections.Generic;

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

		static List<Result> _locationsInSession;
		public static List<Result> LocationsInSession {
			get {
				_locationsInSession = _locationsInSession ?? new List<Result> ();
				return _locationsInSession;
			}
		}

		public App ()
		{
			// The root page of your application
//			var page = FreshPageModelResolver.ResolvePageModel<AddLocationPageModel>();
//			var basicNavContainer = new FreshNavigationContainer (page);
//			MainPage = basicNavContainer;

			var masterDetailNav = new FreshMasterDetailNavigationContainer ();
			masterDetailNav.Init ("Menu");
//			masterDetailNav.AddPage<MapPageModel> ("Home");
			masterDetailNav.AddPage<AddLocationPageModel> ("Add Location");
			masterDetailNav.AddPage<ViewLocationPageModel> ("View Location");
			MainPage = masterDetailNav;

//			MessagingCenter.Subscribe<LocationCellViewModel, Result> (this, "CellTapped", (sender, result) => {
//				System.Diagnostics.Debug.WriteLine("App:: '{0}' was tapped.", result.formatted_address);
//				LocationsInSession.Add(result);
//
//				System.Diagnostics.Debug.WriteLine("Locations searched for so far: {0}", LocationsInSession.Count);
//				foreach(var loc in LocationsInSession) {
//					System.Diagnostics.Debug.WriteLine(loc.formatted_address);
//				}
//
//				// TODO: Save mo rin yung results sa database.
//			});
		}

	}
}

