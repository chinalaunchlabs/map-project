using System;
using Xamarin.Forms.Maps;
using System.Collections.Generic;

namespace Marp
{
	public class ExtendedMap: Map
	{
		private IList<Pin> _staticPins;
		public IList<Pin> StaticPins {
			get { return _staticPins; }
			set {
				_staticPins = value;
				foreach (var pin in value) {
					this.Pins.Add (pin);
				}
			}
		}
	}
}

