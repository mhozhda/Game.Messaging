using Game.Messaging.Server.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Game.Messaging.Server.Controllers
{
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : ControllerBase
	{
		private readonly IDictionary<Type, Func<Exception, IActionResult>> _exceptionHandlers;
		private readonly ILogger<ErrorController> _logger;

		public ErrorController(ILogger<ErrorController> logger)
		{
			_exceptionHandlers = new Dictionary<Type, Func<Exception, IActionResult>>
			{
				{ typeof(ApplicationValidationException), HandleApplicationValidationException },
				{ typeof(NotFoundException), HandleNotFoundException },
			};
			_logger = logger;
		}

		[Route("/error")]
		public IActionResult Error()
		{
			var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

			_logger.LogError(context.Error, $"Error occured in {Assembly.GetExecutingAssembly().GetName().Name}");

			return HandleException(context.Error);
		}

		private IActionResult HandleException(Exception exception)
		{
			Type type = exception.GetType();
			if (_exceptionHandlers.ContainsKey(type))
			{
				return _exceptionHandlers[type].Invoke(exception);
			}

			return HandleUnknownException(exception);
		}

		private IActionResult HandleNotFoundException(Exception exception)
		{
			var ex = exception as NotFoundException;

			var details = new ProblemDetails()
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
				Title = "The specified resource was not found.",
				Detail = ex.Message
			};

			return new NotFoundObjectResult(details);
		}

		private IActionResult HandleApplicationValidationException(Exception exception)
		{
			var ex = exception as ApplicationValidationException;

			var details = new ValidationProblemDetails(ex.Errors)
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
				Detail = ex.Message
			};

			return new BadRequestObjectResult(details);
		}

		private IActionResult HandleUnknownException(Exception exception)
		{
			var details = new ProblemDetails
			{
				Status = StatusCodes.Status500InternalServerError,
				Title = "An error occurred while processing your request.",
				Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
				Detail = "Exception occured"
			};

			return new ObjectResult(details)
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};
		}
	}
}
