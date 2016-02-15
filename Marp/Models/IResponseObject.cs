using System;
using System.Threading;

namespace RestService
{
	public interface IResponseObject
	{
		void Serialize(string content);	
		void HandleException(Exception e, CancellationTokenSource source);
	}
}

