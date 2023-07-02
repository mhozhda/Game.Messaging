using Game.Messaging.Server.Application.Exceptions;
using Game.Messaging.Server.Entities;
using Game.Messaging.Server.Infrastructure.Persistance;
using Game.Messaging.Server.Infrastructure.UserNotifications;
using MediatR;

namespace Game.Messaging.Server.Application.GameEvents.Commands
{
	public static class AddGameEvent
	{
		public record Command : IRequest
		{
			public string Name { get; init; }
			public DateTime StartsAt { get; init; }
			public DateTime ExpiresAt { get; init; }
			public int EventType { get; init; }
			public string Team { get; set; }
		}

		class Handler : IRequestHandler<Command>
		{
			private readonly IRepository<GameEvent> _gameEventsRepository;
			private readonly IUserNotificationService _userNotificationService;

			public Handler(IRepository<GameEvent> gameEventsRepository, IUserNotificationService userNotificationService)
			{
				_gameEventsRepository = gameEventsRepository;
				_userNotificationService = userNotificationService;
			}

			public async Task Handle(Command request, CancellationToken cancellationToken)
			{
				if (await _gameEventsRepository.AnyAsync(x => x.Name == request.Name))
				{
					throw new ApplicationValidationException(nameof(GameEvent.Name), $"Game Event with name {request.Name} Already exists");
				}

				var gameEvent = new GameEvent
				{
					Name = request.Name,
					StartsAt = request.StartsAt,
					ExpiresAt = request.ExpiresAt,
					EventType = request.EventType
				};

				await _gameEventsRepository.AddAsync(gameEvent);

				await _userNotificationService.SendGameEventToTeamAsync(gameEvent, request.Team);
			}
		}
	}
}
