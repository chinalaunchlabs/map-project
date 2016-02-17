using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using FreshMvvm;
using Marp.Models;
using Marp.Geocoder;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using PropertyChanged;
using System.Collections.Generic;

namespace Marp
{
	// TODO: Clean up this really ugly piece of code.
	[ImplementPropertyChanged]
	public class MapPageModel: FreshBasePageModel
	{
		private const int _maxListItems = 3;
		private const int _listRowHeight = 50;
		public MapPageModel() {
			MessagingCenter.Subscribe<LocationCellViewModel, MyLocation> (this, "CellTapped", async (sender, result) => {
				// save to session
				App.LocationsInSession.Add(result);

				// save to database
//				App.Database.SaveLocation(result);

				// clear search and hide results
				IsListVisible = false;
//				SearchAddress = "";

				// update map
				ObservableCollection<MyLocation> tmp = new ObservableCollection<MyLocation>();
				foreach (var loc in App.LocationsInSession) {
					tmp.Add(loc);
				}
				Pins = tmp;
				LocationFocus = result;
			});
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
		public ICommand TextChangedEvent {
			get {
				return new Command (() => {
//					System.Diagnostics.Debug.WriteLine("MapPageModel::Text changed event fired.");
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
				});
			}
		}

		public ICommand SearchBarUnfocused {
			get {
				return new Command (() => {
					System.Diagnostics.Debug.WriteLine("MapPageModel::Search bar has been untapped.");
					IsListVisible = false;
				});
			}
		}

		public ICommand GoToLocCommand {
			get {
				return new Command (() => {
					System.Diagnostics.Debug.WriteLine("MapPageModel::GoToLocCommand placeholder.");
				});
			}
		}

		public ICommand SearchCommand {
			get {
				return new Command (async() => {
					System.Diagnostics.Debug.WriteLine("Fetching results...");
					LoadingResults = true;
					List<MyLocation> results = await App.GeocoderClient.FetchLocations(SearchAddress);
					System.Diagnostics.Debug.WriteLine("Got results...");
					LocationSuggestions = new ObservableCollection<LocationCellViewModel>();
					foreach (var result in results) {
						System.Diagnostics.Debug.WriteLine("loc: {0}", result.Address);
						LocationSuggestions.Add(new LocationCellViewModel(result));
					}
					ListHeight = Math.Min(LocationSuggestions.Count, _maxListItems) * _listRowHeight;
					LoadingResults = false;
					IsListVisible = true;
				});
			}
		}

//		private object _textChangedEv = null;
//		public object TextChangedEvent {
//			get { return _textChangedEv; }
//			set {
//				_textChangedEv = value;
//				System.Diagnostics.Debug.WriteLine ("MapPageModel::Text changed event fired.");
//			}
//		}
//
//		private MyLocation _location;
//		private MapPage _mapPage;
//		private Random _rng;
//
//		public MapPageModel() {
//		}
//
//		private ObservableCollection<MyLocation> _pins;
//		public ObservableCollection<MyLocation> Pins {
//			get { return _pins ?? (_pins = new ObservableCollection<MyLocation>()); }
//			set {
//				_pins = value;
//			}
//		}
//
//		protected override void ViewIsAppearing(object sender, System.EventArgs e) {
//
//			_mapPage = (MapPage)CurrentPage;
//			_location = _mapPage.result;
//
//			System.Diagnostics.Debug.WriteLine ("MapPageModel::map == null ? {0}", _mapPage.map == null);
//
//			if (_location != null) {
//				var locations = App.LocationsInSession;
//				Position p = _location.Position;
//
//				_mapPage.map.MoveToRegion (new MapSpan (p, 0.5, 0.5));
//				_mapPage.map.Pins.Add (new Pin() {
//					Label = _location.Address,
//					Position = _location.Position
//				});
//				foreach (var location in locations) {
//					_mapPage.map.Pins.Add (new Pin () {
//						Label = location.Address,
//						Position = location.Position
//					});
//				}
//			}
//
//			base.ViewIsAppearing (sender, e);
//		}
	}
}

