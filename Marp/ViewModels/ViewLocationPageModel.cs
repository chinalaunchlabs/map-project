using System;
using FreshMvvm;
using System.Collections.Generic;
using Xamarin.Forms;
using Marp.Models;
using System.Windows.Input;
using PropertyChanged;

namespace Marp
{
	[ImplementPropertyChanged]
	public class ViewLocationPageModel: FreshBasePageModel
	{

		public ViewLocationPageModel() {
			System.Diagnostics.Debug.WriteLine ("ViewLocationPageModel::Initialization");

			LoadLocations ();

			MessagingCenter.Subscribe <LocationCellViewModel, MyLocation> (this, "CellTapped2", async (sender, location) => {
				System.Diagnostics.Debug.WriteLine("ViewLocationPageModel:: :(");
				MapPage page = (MapPage) FreshPageModelResolver.ResolvePageModel<MapPageModel>();
				page.result = location;
				await CurrentPage.Navigation.PushAsync(page);
			});
		}

		void LoadLocations() {
			SavedLocations = new List<LocationCellViewModel> ();
			foreach (var location in App.Database.GetLocations()) {
				SavedLocations.Add (new LocationCellViewModel (location));
			}
		}

		protected override void ViewIsAppearing (object sender, EventArgs e)
		{
			LoadLocations ();
			base.ViewIsAppearing (sender, e);
		}

		private List<LocationCellViewModel> _savedLocations;
		public List<LocationCellViewModel> SavedLocations {
			get { return _savedLocations; }
			set {
				_savedLocations = value;

			}
		}

		public ICommand ClearLocationsCommand {
			get {
				return new Command (() => {
					App.Database.DeleteAllLocations();
					LoadLocations();
				});
			}
		}
	}
}

