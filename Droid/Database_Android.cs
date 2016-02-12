using System;
using System.IO;
using Xamarin.Forms;
using SQLite;
using Marp;
using Marp.Droid;

[assembly: Dependency(typeof(Database_Android))]
namespace Marp.Droid
{
	public class Database_Android : IDatabase
	{
		public Database_Android ()
		{
		}

		public SQLiteConnection DBConnect () {
			var filename = "Marp.db3";
			string folder = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var path = Path.Combine (folder, filename);
			var connection = new SQLiteConnection (path);
			return connection;
		}
	}
}

