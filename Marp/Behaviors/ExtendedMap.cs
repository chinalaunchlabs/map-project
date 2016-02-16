using System;
using Xamarin.Forms.Maps;
using System.Collections.Generic;
using Xamarin.Forms;
using Marp.Models;

namespace Marp.Behaviors
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

		public static readonly BindableProperty FocusOnProperty =
			BindableProperty.Create<ExtendedMapBehavior, MyLocation> (
				p => p.FocusOn, null, BindingMode.TwoWay, null, FocusOnSourceChanged
			);

		public MyLocation FocusOn {
			get { return (MyLocation)GetValue (FocusOnProperty); }
			set { SetValue (FocusOnProperty, value); }
		}

		private static void FocusOnSourceChanged(BindableObject bindable, MyLocation oldVal, MyLocation newVal) {
			var behavior = bindable as ExtendedMapBehavior;
			if (behavior == null)
				return;
			behavior.MoveToRegion ();
		}

		private void MoveToRegion() {
			System.Diagnostics.Debug.WriteLine ("ExtendedMapBehavior::MoveToRegion()");
			if (FocusOn == null)
				return;

			MyLocation location = FocusOn;

			System.Diagnostics.Debug.WriteLine ("ExtendedMapBehavior::Focusing on {0}, {1}", location.Latitude, location.Longitude );

			_map.MoveToRegion (new MapSpan (location.Position, 0.01, 0.01));
		}

		private void AddPins() {
			System.Diagnostics.Debug.WriteLine ("ExtendedMapBehavior::AddPins()");
			if (ItemsSource == null)
				return;

			_map.Pins.Clear ();

			foreach (var item in ItemsSource) {
				System.Diagnostics.Debug.WriteLine ("ExtendedMapBehavior::Adding {0}", item.Address);
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

