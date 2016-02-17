using System;
using FreshMvvm;
using System.Collections.Generic;
using Xamarin.Forms;
using Marp.Models;
using System.Windows.Input;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;

namespace Marp
{
	[ImplementPropertyChanged]
	public class ViewLocationPageModel: FreshBasePageModel
	{

		private const int _maxListItems = 3;
		private const int _listRowHeight = 50;
		public ViewLocationPageModel() {
			LoadLocations ();
		}

		void LoadLocations() {
			var tmp = new ObservableCollection<LocationCellViewModel> ();
			foreach (var location in App.Database.GetLocations()) {
				tmp.Add (new LocationCellViewModel (location));
			}
			SavedLocations = tmp;
		}

		protected override void ViewIsAppearing (object sender, EventArgs e)
		{
			LoadLocations ();
			MessagingCenter.Subscribe<LocationCellViewModel, MyLocation> (this, "LocationTapped", async (sndr, result) => {
				LocationFocus = result;
				// TODO: Maybe get this working within the FreshMvvm framework?
				ViewSinglePageModel vm = new ViewSinglePageModel(result);
				ViewSinglePage page = new ViewSinglePage();
				page.BindingContext = vm;
				vm.CurrentPage = page;

				await CurrentPage.Navigation.PushModalAsync(page);

//				var page = FreshPageModelResolver.ResolvePageModel<ViewSinglePageModel>(result);
//				await CurrentPage.Navigation.PushModalAsync(page);
			});

			base.ViewIsAppearing (sender, e);
		}

		protected override void ViewIsDisappearing (object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine ("MapPageModel::View is disappearing");
			MessagingCenter.Unsubscribe<LocationCellViewModel, MyLocation> (this, "LocationTapped");
			base.ViewIsDisappearing (sender, e);
		}

		private string _searchAddress;
		public string SearchAddress {
			get { return _searchAddress; }
			set { _searchAddress = value; }
		}

		private ObservableCollection<LocationCellViewModel> _savedLocations;
		public ObservableCollection<LocationCellViewModel> SavedLocations {
			get { return _savedLocations ?? (_savedLocations = new ObservableCollection<LocationCellViewModel>()); }
			set {
				_savedLocations = value;

			}
		}

		private MyLocation _locationFocus;
		public MyLocation LocationFocus {
			get { return _locationFocus; }
			set { _locationFocus = value; }
		}
			

		public ICommand ClearLocationsCommand {
			get {
				return new Command (() => {
					App.Database.DeleteAllLocations();
					LoadLocations();
				});
			}
		}

		List<MyLocation> _oldFiltered = new List<MyLocation>();
		public ICommand TextChangedEvent {
			get {
				return new Command ((dfsdf) => {
					//					System.Diagnostics.Debug.WriteLine("MapPageModel::Text changed event fired.");
					var partialQuery = SearchAddress;
					List<MyLocation> savedLocations = App.Database.GetLocations();
					List<MyLocation> filteredList = savedLocations.Where(l => l.Address.Contains(partialQuery)).ToList();

					// if filtered locations changed, saka baguhin
					// otherwise jittery yung pag-refresh ng listview kung di naman talaga nagbabago yung dataset
					// TODO: Fix this, the condition doesn't work.
					if (!filteredList.All(_oldFiltered.Contains)) {
						SavedLocations = new ObservableCollection<LocationCellViewModel> ();
						foreach (var location in filteredList) {
							SavedLocations.Add (new LocationCellViewModel (location));
						}
						_oldFiltered = filteredList;
					}
				});
			}
		}

	}
}

