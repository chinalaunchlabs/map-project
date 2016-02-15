using System;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace Marp
{
	[ImplementPropertyChanged]
	public class AddLocationPageModel: FreshBasePageModel
	{
		public AddLocationPageModel() {
	
		}

		private string _searchAddress;
		public string SearchAddress {
			get { return _searchAddress; }
			set { 
				_searchAddress = value;
			}
		}

		private List<string> _searchResults;
		public List<string> SearchResults {
			get { return _searchResults; }
			set {
				_searchResults = value;
			}
		}

		public ICommand SearchCommand {
			get {
				return new Command (async (sdf) => {
					var results = await App.GeocoderClient.FetchLocations(SearchAddress);
					SearchResults = new List<string>();
					foreach (var result in results) {
						SearchResults.Add(result.formatted_address);
					}
				});
			}
		}
	}
}

