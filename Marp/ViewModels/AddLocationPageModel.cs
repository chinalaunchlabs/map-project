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
				if (value != null) {

				}
			}
		}

		public ICommand SearchCommand {
			get {
				return new Command (async (sdf) => {
					App.GeocoderClient.FetchLocations(SearchAddress);
//					var client = new HttpClient();
//					client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/");
//					var response = await client.GetAsync(string.Format ("json?address={0}&key={1}", WebUtility.UrlEncode(SearchAddress), "AIzaSyBED447FFVqdLwJizxQpUAqcvDj4brgx1c"));
//					var json = await response.Content.ReadAsStringAsync();
//					System.Diagnostics.Debug.WriteLine(json);
				});
			}
		}
	}
}

