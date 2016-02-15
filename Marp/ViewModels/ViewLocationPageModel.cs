using System;
using FreshMvvm;
using System.Collections.Generic;

namespace Marp
{
	public class ViewLocationPageModel: FreshBasePageModel
	{
		private List<string> _savedLocations = new List<string> {
			"Apple", "Baboy", "Cat"
		};
		public List<string> SavedLocations {
			get { return _savedLocations; }
		}
	}
}

