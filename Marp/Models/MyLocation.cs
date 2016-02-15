using System;
using SQLite;
using Xamarin.Forms.Maps;
using Marp.Geocoder;

namespace Marp.Models
{
	public class MyLocation
	{
		public MyLocation() {

		}

		public MyLocation(Result r) {
			Address = r.formatted_address;
			Latitude = r.geometry.location.lat;
			Longitude = r.geometry.location.lng;
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Address { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		[Ignore]
		public Position Position {
			get {
				return new Position (Latitude, Longitude);
			}
		}
	}
}

