using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Marp.Models;

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

		public ICommand PopPageCommand {
			get {
				return new Command(() => {
					// TODO: Reimplement in FreshMvvm framework when I know how to.
					CurrentPage.Navigation.PopModalAsync();
				});
			}
		}
	}
}

