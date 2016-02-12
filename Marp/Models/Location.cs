using System;
using SQLite;

namespace Marp
{
	public class Location
	{
		public int ID { get; set; }
		public string Address { get; set; }
		public Coordinate Coord { get; set; }
	}
}

