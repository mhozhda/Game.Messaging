namespace Game.Messaging.Client.Exceptions
{
	public class RemoteServiceRequestException : Exception
	{
		public RemoteServiceRequestException()
		{
		}

		public RemoteServiceRequestException(string? message) : base(message)
		{
		}

		public RemoteServiceRequestException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
