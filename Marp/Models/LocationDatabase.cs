using System;
using Marp.Models;
using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Marp.Models
{
	public class LocationDatabase
	{
		protected static object locker = new object();
		protected SQLiteConnection database;

		public LocationDatabase ()
		{
			database = DependencyService.Get<IDatabase> ().DBConnect ();
			database.CreateTable<MyLocation> ();
		}

		public List<MyLocation> GetLocations() {
			lock (locker) {
				return database.Table<MyLocation> ().ToList();
			}
		}

		public MyLocation GetLocation( int id ) {
			lock (locker) {
				return database.Table<MyLocation> ().FirstOrDefault (x => x.ID == id);
			}
		}

		public int SaveLocation(MyLocation loc) {
			lock (locker) {
				var tmp = database.Table<MyLocation>().ToList ();
				bool flag = false;
				System.Diagnostics.Debug.WriteLine ("loc.Address: {0}", loc.Address);
				foreach (MyLocation t in tmp) {
					System.Diagnostics.Debug.WriteLine ("t.Address: {0}", t.Address);
					if (t.Address == loc.Address)
						flag = true;
				}
				
				if (flag) {
					System.Diagnostics.Debug.WriteLine ("LocationDatabase::Place already in db, not saving.");
//					database.Update (loc);
					return loc.ID;
				} else {
					return database.Insert (loc);
				}
			}
		}

		public int DeleteAllLocations() {
			lock (locker) {
				return database.DeleteAll<MyLocation> ();
			}
		}
	}
}

