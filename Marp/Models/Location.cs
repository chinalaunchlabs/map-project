using System;
using SQLite;
using Xamarin.Forms.Maps;

namespace Marp.Models
{
	public class Location
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Address { get; set; }
		public Position Position { get; set; }
	}
}

