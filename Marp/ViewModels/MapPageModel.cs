using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using FreshMvvm;
using Marp.Models;
using Marp.Geocoder;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using PropertyChanged;
using AdvancedTimer.Forms.Plugin.Abstractions;

namespace Marp
{
	// TODO: Clean up this really ugly piece of code.
	[ImplementPropertyChanged]
	public class MapPageModel: FreshBasePageModel
	{
		private const int _maxListItems = 3;
		private const int _listRowHeight = 50;
		private bool _starTapped = false;
		IAdvancedTimer timer;

		public MapPageModel() {
			timer = DependencyService.Get<IAdvancedTimer> ();
			timer.initTimer (1000, timerElapsed, true);
		}

		public void timerElapsed(object obj, EventArgs e) {

		}

		protected override void ViewIsAppearing (object sender, EventArgs e)
		{
			MessagingCenter.Subscribe<LocationCellViewModel, MyLocation> (this, "LocationTapped", (sndr, result) => {
				// save to session
				App.LocationsInSession.Add(result);

				// hide results
				IsListVisible = false;

				// update map
				ObservableCollection<MyLocation> tmp = new ObservableCollection<MyLocation>();
				foreach (var loc in App.LocationsInSession) {
					tmp.Add(loc);
				}
				Pins = tmp;
				LocationFocus = result;
				_starTapped = false;
			});

			MessagingCenter.Subscribe<LocationCellViewModel, MyLocation> (this, "StarTapped", (sndr, location) => {
				System.Diagnostics.Debug.WriteLine("MapPageModel::Star was tapped.");
				_starTapped = true;
			});
			base.ViewIsAppearing (sender, e);

			MessagingCenter.Subscribe<GoogleGeocoder>(this, "NetworkError", async (obj) => {
				await CoreMethods.DisplayAlert("Network Error", "Something went wrong. Please try your request again.", "OK");
			});
		}

		protected override void ViewIsDisappearing (object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine ("MapPageModel::View is disappearing");
			MessagingCenter.Unsubscribe<LocationCellViewModel, MyLocation> (this, "LocationTapped");
			base.ViewIsDisappearing (sender, e);
		}

		/* Exposed Properties */
		private string _searchAddress;
		public string SearchAddress {
			get { return _searchAddress; }
			set { _searchAddress = value; }
		}
			
		private bool _loadingResults;
		public bool LoadingResults {
			get { return _loadingResults; }
			set { _loadingResults = value; }
		}

		// List properties
		private ObservableCollection<LocationCellViewModel> _locationSuggestions;
		public ObservableCollection<LocationCellViewModel> LocationSuggestions {
			get { return _locationSuggestions ?? (_locationSuggestions = new ObservableCollection<LocationCellViewModel>()); }
			set {
				_locationSuggestions = value;
			}
		}

		private bool _isListVisible;
		public bool IsListVisible {
			get { return _isListVisible; }
			set { _isListVisible = value; }
		}

		public int ListRowHeight {
			get { return _listRowHeight; }
		}

		private int _listHeight = 0;
		public int ListHeight {
			get { 
				return _listHeight;
			}
			set {
				_listHeight = value;
			}
		}

		// Map properties
		// bound to ExtendedMapBehavior.ItemsSource
		private ObservableCollection<MyLocation> _pins;
		public ObservableCollection<MyLocation> Pins {
			get { return _pins ?? (_pins = new ObservableCollection<MyLocation>()); }
			set { _pins = value; }
		}

		private MyLocation _locationFocus;
		public MyLocation LocationFocus {
			get { return _locationFocus; }
			set { _locationFocus = value; }
		}

		/* Commands */
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
						LocationSuggestions = new ObservableCollection<LocationCellViewModel> ();
						foreach (var location in filteredList) {
							LocationSuggestions.Add (new LocationCellViewModel (location));
						}
						ListHeight = Math.Min(LocationSuggestions.Count, _maxListItems) * _listRowHeight;
						RaisePropertyChanged("ListHeight");
						_oldFiltered = filteredList;
					}
				});
			}
		}

		public ICommand SearchBarFocused {
			get {
				return new Command (() => {
					System.Diagnostics.Debug.WriteLine("MapPageModel::Search bar has been tapped.");

					if (SearchAddress == "" || SearchAddress == null) {
						// nothing has been input yet
						LocationSuggestions = new ObservableCollection<LocationCellViewModel> ();
						foreach (var location in App.Database.GetLocations()) {
							LocationSuggestions.Add (new LocationCellViewModel (location));
						}
						ListHeight = Math.Min(LocationSuggestions.Count, _maxListItems) * _listRowHeight;
						IsListVisible = true;
					} 
					else {
						IsListVisible = true;
					}
				});
			}
		}

		public ICommand SearchBarUnfocused {
			get {
				return new Command (() => {
					System.Diagnostics.Debug.WriteLine("MapPageModel::Search bar has gone out of focus.");
					System.Diagnostics.Debug.WriteLine("MapPageModel::_starTapped = {0}", _starTapped);

					if (_starTapped) {
						System.Diagnostics.Debug.WriteLine("MapPageModel::But a star was tapped so not hiding list.");
						_starTapped = false;
					}
					else {
						System.Diagnostics.Debug.WriteLine("MapPageModel::Hiding list.");
						IsListVisible = false;
					}
				});
			}
		}
			
		public ICommand SearchCommand {
			get {
				return new Command (async() => {
					IsListVisible = false;
					LoadingResults = true;
					List<MyLocation> results = await App.GeocoderClient.FetchLocations(SearchAddress);
					LocationSuggestions = new ObservableCollection<LocationCellViewModel>();
					foreach (var result in results) {
						LocationSuggestions.Add(new LocationCellViewModel(result));
					}
					ListHeight = Math.Min(LocationSuggestions.Count, _maxListItems) * _listRowHeight;
					LoadingResults = false;
					IsListVisible = true;
				});
			}
		}
	}
}

