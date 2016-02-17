using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Marp.Geocoder;
using Marp.Models;

namespace Marp
{
	public partial class MapPage : ContentPage
	{
		public Map map;
		public MyLocation result;
		public MapPage ()
		{
			InitializeComponent ();
		}
	}
}

