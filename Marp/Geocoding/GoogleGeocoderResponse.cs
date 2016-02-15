using System;
using China.RestClient;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Marp.Geocoder
{
	public class GoogleGeocoderResponse: IResponseObject
	{
		private GoogleGeocoderResponseObj response;
		public void Serialize(string content) {
			response = JsonConvert.DeserializeObject<GoogleGeocoderResponseObj> (content);
			System.Diagnostics.Debug.WriteLine ("Serialize::" + response.results[0].formatted_address);
		}

		public void HandleException(Exception e, CancellationTokenSource source) {
			Type exceptionType = e.GetType ();
			System.Diagnostics.Debug.WriteLine ("HandleException::{0}", exceptionType);
			if (exceptionType == typeof(TaskCanceledException)) {
				TaskCanceledException tce = (TaskCanceledException)e;
				if (tce.CancellationToken == source.Token) {
					System.Diagnostics.Debug.WriteLine ("TodoResponse::This is a real cancellation triggered by the caller.");
				} else {
					System.Diagnostics.Debug.WriteLine ("TodoResponse::This is a web request time out.");
				}
			}
			response = new GoogleGeocoderResponseObj ();
			response.status = "error";
			response.results = null;
		}

		public string Results {
			get { return response.results.ToString(); }
		}
	}

	class GoogleGeocoderResponseObj {
		public List<Result> results { get; set; }
		public string status { get; set; }
	}
}

