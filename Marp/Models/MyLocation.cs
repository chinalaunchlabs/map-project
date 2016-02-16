using System;
using SQLite;
using Xamarin.Forms.Maps;
using Marp.Geocoder;
using PropertyChanged;

namespace Marp.Models
{
	[ImplementPropertyChanged]
	public class MyLocation
	{
		public MyLocation() {

		}

		public MyLocation(string label, Position p) {
			Address = label;
			Latitude = p.Latitude;
			Longitude = p.Longitude;
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

