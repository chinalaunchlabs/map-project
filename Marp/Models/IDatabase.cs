using SQLite;

namespace Marp
{
	public interface IDatabase
	{
		SQLiteConnection DBConnect();
	}
}

