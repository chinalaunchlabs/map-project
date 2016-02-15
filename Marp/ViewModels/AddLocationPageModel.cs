using System;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marp
{
	[ImplementPropertyChanged]
	public class AddLocationPageModel: FreshBasePageModel
	{
		private Geocoder _geocoder;
		public AddLocationPageModel() {
			_geocoder = new Geocoder ();
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
				return new Command ((sdf) => {
					var results = _geocoder.GetPositionsForAddressAsync(SearchAddress);
//					foreach (Position result in results)
						System.Diagnostics.Debug.WriteLine(results);
				});
			}
		}
	}
}

