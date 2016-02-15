using System;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marp.Geocoder;

namespace Marp
{
	[ImplementPropertyChanged]
	public class AddLocationPageModel: FreshBasePageModel
	{
		public AddLocationPageModel() {
			MessagingCenter.Subscribe<LocationCellViewModel, Result> (this, "CellTapped", async (sender, result) => {
//				await CoreMethods.PushPageModel<MapPageModel>(result);
				MapPage page = (MapPage) FreshPageModelResolver.ResolvePageModel<MapPageModel>();
				page.result = result;
				await CurrentPage.Navigation.PushAsync(page);
			});
		}

		private string _searchAddress;
		public string SearchAddress {
			get { return _searchAddress; }
			set { 
				_searchAddress = value;
			}
		}

		private List<LocationCellViewModel> _searchResults;
		public List<LocationCellViewModel> SearchResults {
			get { return _searchResults; }
			set {
				_searchResults = value;
			}
		}

		public ICommand SearchCommand {
			get {
				return new Command (async (sdf) => {
					var results = await App.GeocoderClient.FetchLocations(SearchAddress);
					SearchResults = new List<LocationCellViewModel>();
					foreach (var result in results) {
						SearchResults.Add(new LocationCellViewModel(result));
					}
				});
			}
		}
	}
}

