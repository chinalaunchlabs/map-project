using System;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marp.Geocoder;
using Marp.Models;
using System.Collections.ObjectModel;

namespace Marp
{
	[ImplementPropertyChanged]
	public class AddLocationPageModel: FreshBasePageModel
	{
		public AddLocationPageModel() {
			MessagingCenter.Subscribe<LocationCellViewModel, MyLocation> (this, "CellTapped", async (sender, result) => {
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
			
		private ObservableCollection<LocationCellViewModel> _searchResults;
		public ObservableCollection<LocationCellViewModel> SearchResults {
			get { return _searchResults; }
			set {
				_searchResults = value;
			}
		}

		public ICommand SearchCommand {
			get {
				return new Command (async (sdf) => {
					List<MyLocation> results = await App.GeocoderClient.FetchLocations(SearchAddress);
					SearchResults = new ObservableCollection<LocationCellViewModel>();
					foreach (var result in results) {
						SearchResults.Add(new LocationCellViewModel(result));
					}
				});
			}
		}

		protected override void ViewIsDisappearing (object sender, EventArgs e)
		{
			// clear search
			SearchAddress = "";
			SearchResults.Clear ();

			base.ViewIsDisappearing (sender, e);
		}
	}
}

