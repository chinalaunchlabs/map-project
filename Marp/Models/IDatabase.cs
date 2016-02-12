using SQLite;

namespace CloneDo.Mvvm
{
	public interface IDatabase
	{
		SQLiteConnection DBConnect();
	}
}

