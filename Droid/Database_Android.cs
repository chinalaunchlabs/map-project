using System;
using System.IO;
using CloneDo.Mvvm;
using CloneDo.Mvvm.Droid;
using Xamarin.Forms;
using SQLite;

[assembly: Dependency(typeof(Database_Android))]
namespace CloneDo.Mvvm.Droid
{
	public class Database_Android : IDatabase
	{
		public Database_Android ()
		{
		}

		public SQLiteConnection DBConnect () {
			var filename = "CloneDo.db3";
			string folder = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			var path = Path.Combine (folder, filename);
			var connection = new SQLiteConnection (path);
			return connection;
		}
	}
}

