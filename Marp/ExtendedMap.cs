using System;
using Xamarin.Forms.Maps;
using System.Collections.Generic;
using Xamarin.Forms;
using Marp.Models;

namespace Marp
{
	public class ExtendedMapBehavior: Behavior<Map>
	{
		private Map _map;

		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create<ExtendedMapBehavior, IEnumerable<MyLocation>> (
				p => p.ItemsSource, null, BindingMode.TwoWay, null, ItemsSourceChanged
			);

		public IEnumerable<MyLocation> ItemsSource {
			get { return (IEnumerable<MyLocation>)GetValue (ItemsSourceProperty); }
			set { SetValue (ItemsSourceProperty, value); }
		}

		private static void ItemsSourceChanged(BindableObject bindable, IEnumerable<MyLocation> oldVal, IEnumerable<MyLocation> newVal) {
			var behavior = bindable as ExtendedMapBehavior;
			if (behavior == null)
				return;
			behavior.AddPins ();
		}

		private void AddPins() {
			System.Diagnostics.Debug.WriteLine ("ExtendedMapBehavior::AddPins()");
			_map.Pins.Clear ();
			if (ItemsSource == null)
				return;
			foreach (var item in ItemsSource) {
				_map.Pins.Add (new Pin() {
					Label = item.Address,
					Position = item.Position
				});
			}
		}

		protected override void OnAttachedTo (Map bindable)
		{
			base.OnAttachedTo (bindable);
			bindable.BindingContextChanged += (sender, e) => {
				BindingContext = _map.BindingContext;
			};
			_map = bindable;
		}
	}
}

