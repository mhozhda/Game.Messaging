using Game.Messaging.Server.Application.Exceptions;
using Game.Messaging.Server.Entities;
using Game.Messaging.Server.Infrastructure.Persistance;
using Game.Messaging.Server.Infrastructure.UserNotifications;
using MediatR;

namespace Game.Messaging.Server.Application.GameOffers.Commands
{
	public static class AddGameOffer
	{
		public record Command : IRequest
		{
			public string Name { get; init; }
			public DateTime StartsAt { get; init; }
			public DateTime ExpiresAt { get; init; }
			public int OfferType { get; init; }
			public string Team { get; set; }
		}

		class Handler : IRequestHandler<Command>
		{
			private readonly IRepository<GameOffer> _gameOffersRepository;
			private readonly IUserNotificationService _userNotificationService;

			public Handler(IRepository<GameOffer> gameOffersRepository, IUserNotificationService userNotificationService)
			{
				_gameOffersRepository = gameOffersRepository;
				_userNotificationService = userNotificationService;
			}

			public async Task Handle(Command request, CancellationToken cancellationToken)
			{
				if (await _gameOffersRepository.AnyAsync(x => x.Name == request.Name))
				{
					throw new ApplicationValidationException(nameof(GameOffer.Name), $"Offer with name {request.Name} Already exists");
				}

				var gameOffer = new GameOffer
				{
					Name = request.Name,
					StartsAt = request.StartsAt,
					ExpiresAt = request.ExpiresAt,
					OfferType = request.OfferType
				};

				await _gameOffersRepository.AddAsync(gameOffer);

				await _userNotificationService.SendGameOfferToTeamAsync(gameOffer, request.Team);
			}
		}
	}
}
