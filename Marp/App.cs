using System;

using Xamarin.Forms;
using FreshMvvm;
using Marp.Geocoder;

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

		public App ()
		{
			// The root page of your application
//			var page = FreshPageModelResolver.ResolvePageModel<AddLocationPageModel>();
//			var basicNavContainer = new FreshNavigationContainer (page);
//			MainPage = basicNavContainer;

			var masterDetailNav = new FreshMasterDetailNavigationContainer ();
			masterDetailNav.Init ("Menu");
			masterDetailNav.AddPage<AddLocationPageModel> ("Add Location");
			masterDetailNav.AddPage<ViewLocationPageModel> ("View Location");
			MainPage = masterDetailNav;
		}

	}
}

