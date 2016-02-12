using System;
using SQLite;
using System.IO;
using Xamarin.Forms;
using Marp;
using Marp.iOS;

[assembly: Dependency(typeof(Database_iOS))]
namespace Marp.iOS
{
	public class Database_iOS : IDatabase
	{
		public Database_iOS() { }
		public SQLiteConnection DBConnect()
		{
			var filename = "Marp.db3";
			string folder = 
				Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string libraryFolder = Path.Combine (folder, "..", "Library"); 
			var path = Path.Combine(libraryFolder, filename);
			var connection = new SQLiteConnection(path);
			return connection;
		}
	}
}