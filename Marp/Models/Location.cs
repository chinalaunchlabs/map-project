using System;
using SQLite;

namespace Marp.Models
{
	public class Location
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Address { get; set; }
		public Coordinate Coord { get; set; }
	}
}

