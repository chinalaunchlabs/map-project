using System;
using SQLite;
using System.IO;
using Xamarin.Forms;
using CloneDo.Mvvm;
using CloneDo.Mvvm.iOS;

[assembly: Dependency(typeof(Database_iOS))]
namespace CloneDo.Mvvm.iOS
{
	public class Database_iOS : IDatabase
	{
		public Database_iOS() { }
		public SQLiteConnection DBConnect()
		{
			var filename = "CloneDo.db3";
			string folder = 
				Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string libraryFolder = Path.Combine (folder, "..", "Library"); 
			var path = Path.Combine(libraryFolder, filename);
			var connection = new SQLiteConnection(path);
			return connection;
		}
	}
}