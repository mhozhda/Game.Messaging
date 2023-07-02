namespace Game.Messaging.Server.Application.Exceptions
{
	public class ApplicationValidationException : Exception
	{
		public ApplicationValidationException()
			: base("One or more validation failures have occurred.")
		{
			Errors = new Dictionary<string, string[]>();
		}

		public ApplicationValidationException(string propertyName, string errorMessage)
			: this()
		{
			Errors = new Dictionary<string, string[]>();
			Errors.Add(propertyName, new[] { errorMessage });
		}

		public ApplicationValidationException(IEnumerable<(string PropertyName, string ErrorMessage)> failures)
			: this()
		{
			Errors = failures
				.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
				.ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
		}

		public IDictionary<string, string[]> Errors { get; }
	}
}
