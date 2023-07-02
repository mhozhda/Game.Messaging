using Game.Messaging.Server.Application.Common.Paging;
using Game.Messaging.Server.Application.GameOffers.Commands;
using Game.Messaging.Server.Application.GameOffers.Dtos;
using Game.Messaging.Server.Application.GameOffers.Queries;
using Game.Messaging.Server.Application.GameOffers.Specifications.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Game.Messaging.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GameOffersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public GameOffersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<PagingResponse<GameOfferDto>>> GetList([FromQuery] GameOffersFilter filter)
		{
			return await _mediator.Send(new GetGameOffers.Query(filter));
		}

		[HttpPost]
		public async Task<ActionResult> AddGameOffer(AddGameOffer.Command command)
		{
			await _mediator.Send(command);

			return Ok();
		}
	}
}
