using Game.Messaging.Server.Application.Common.Paging;
using Game.Messaging.Server.Application.GameEvents.Commands;
using Game.Messaging.Server.Application.GameEvents.Dtos;
using Game.Messaging.Server.Application.GameEvents.Queries;
using Game.Messaging.Server.Application.GameEvents.Specifications.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Game.Messaging.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GameEventsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public GameEventsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<PagingResponse<GameEventDto>>> GetList([FromQuery] GameEventsFilter filter)
		{
			return await _mediator.Send(new GetGameEvents.Query(filter));
		}

		[HttpPost]
		public async Task<ActionResult> AddGameEvent(AddGameEvent.Command command)
		{
			await _mediator.Send(command);

			return Ok();
		}
	}
}
