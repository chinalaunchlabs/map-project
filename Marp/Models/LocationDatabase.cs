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
				return database.Table<MyLocation> ().OrderBy( l => l.Address ).ToList();
			}
		}

		public MyLocation GetLocation( int id ) {
			lock (locker) {
				return database.Table<MyLocation> ().FirstOrDefault (x => x.ID == id);
			}
		}

		public bool InDatabase(string addr) {
			lock (locker) {
				var tmp = database.Query<MyLocation> ("SELECT * FROM MyLocation WHERE Address = ?", addr);
				if (tmp.Any ())
					return true;
				else
					return false;
			}
		}

		public int SaveLocation(MyLocation loc) {
			lock (locker) {				
				if (InDatabase(loc.Address)) {
					System.Diagnostics.Debug.WriteLine ("LocationDatabase::Place already in db, not saving.");
					return loc.ID;
				} else {
					System.Diagnostics.Debug.WriteLine ("LocationDatabase::Saved to db.");
					return database.Insert (loc);
				}
			}
		}

		public int DeleteLocation(int id) {
			lock (locker) {
				return database.Delete<MyLocation> (id);
			}
		}

		public int DeleteAllLocations() {
			lock (locker) {
				return database.DeleteAll<MyLocation> ();
			}
		}
	}
}

