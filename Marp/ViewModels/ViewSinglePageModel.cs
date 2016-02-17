using System;
using FreshMvvm;
using PropertyChanged;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using Marp.Models;
using System.Collections.ObjectModel;

namespace Marp
{
	[ImplementPropertyChanged]
	public class ViewSinglePageModel: FreshBasePageModel
	{
		public ViewSinglePageModel (MyLocation location)
		{
			System.Diagnostics.Debug.WriteLine ("ViewSinglePageModel::Hello");
			Location = location;
			Pin = new ObservableCollection<MyLocation>(){
				location
			};
		}

		private ObservableCollection<MyLocation> _pin;
		public ObservableCollection<MyLocation> Pin {
			get { return _pin; }
			set { _pin = value; }
		}

		private MyLocation _location;
		public MyLocation Location {
			get { return _location; }
			set { _location = value; }
		}

	}
}

