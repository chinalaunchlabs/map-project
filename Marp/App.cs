using System;

using Xamarin.Forms;
using FreshMvvm;

namespace Marp
{
	public class App : Application
	{
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

