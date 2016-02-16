using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using FreshMvvm;
using Marp.Models;
using Marp.Geocoder;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using PropertyChanged;

namespace Marp
{
	// TODO: Clean up this really ugly piece of code.
	[ImplementPropertyChanged]
	public class MapPageModel: FreshBasePageModel
	{

		public MapPageModel() {

		}

		// Locations
		// bound to ExtendedMapBehavior.ItemsSource
		private ObservableCollection<MyLocation> _pins;
		public ObservableCollection<MyLocation> Pins {
			get { return _pins ?? (_pins = new ObservableCollection<MyLocation>()); }
			set { _pins = value; }
		}

		public ICommand TextChangedEvent {
			get {
				return new Command (() => {
					System.Diagnostics.Debug.WriteLine("MapPageModel::Text changed event fired.");
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

