## Marp
The maps app na pinagawa ni Mar.

`#awfulnamingsensibilities`

## Notes
* Add the `Xamarin.Forms.Maps` package to each project in the solution.
	* Once the package is installed, the following initialization code is required in each application project:
	`AppDelegate.cs` for iOS in the `FinishedLaunching` method
	```
	Xamarin.FormsMaps.Init();
	```
	`MainActivity.cs` for Android in the `OnCreate` method:
	```
	Xamarin.FormsMaps.Init(this, bundle);
	```
	* Android emulator needs Gapps. :/
* If also using the Google geocoder, `com.google.android.maps.v2.API_KEY` can be omitted and replaced with `com.google.android.geo.API_KEY`.
