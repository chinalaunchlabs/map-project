using System;
using System.Net;
using China.RestClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Marp.Models;

namespace Marp.Geocoder
{
	/*
	 * Google Geocoding API:
	 * ---------------------
	 * [1] Geocoding request should be of the form 
	 * 		"https://maps.googleapis.com/maps/api/geocode/json?address=<location>&key=<API_KEY>"
	 * [2] Reverse geocoding request should be of the form
	 * 		"https://maps.googleapis.com/maps/api/geocode/json?latlng=<lat>,<lng>&key=<API_KEY>"
	 */ 
	public class GoogleGeocoder
	{
		private string API_KEY = "AIzaSyBED447FFVqdLwJizxQpUAqcvDj4brgx1c";
		private string API_BASE = "https://maps.googleapis.com/maps/api/geocode/";
		private RestService _client;
		public GoogleGeocoder ()
		{
			_client = new RestService ();
		}

		public async Task<List<MyLocation>> FetchLocations(string address) {
			var uri = string.Format ("{0}json?address={1}&key={2}", API_BASE, WebUtility.UrlEncode(address), API_KEY);
			var response = await _client.Get<GoogleGeocoderResponse>(uri);
			List<MyLocation> locations = new List<MyLocation> ();
			foreach (var result in response.Results) {
				locations.Add (new MyLocation (result));
			}
			return locations;
		}
	}
}

