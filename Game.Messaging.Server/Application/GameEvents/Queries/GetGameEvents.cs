using AutoMapper;
using Game.Messaging.Server.Application.Common.Paging;
using Game.Messaging.Server.Application.GameEvents.Dtos;
using Game.Messaging.Server.Application.GameEvents.Specifications;
using Game.Messaging.Server.Application.GameEvents.Specifications.Filters;
using Game.Messaging.Server.Entities;
using Game.Messaging.Server.Infrastructure.Persistance;
using MediatR;

namespace Game.Messaging.Server.Application.GameEvents.Queries
{
	public static class GetGameEvents
	{
		public record Query(GameEventsFilter Filter) : IRequest<PagingResponse<GameEventDto>>;

		class Handler : IRequestHandler<Query, PagingResponse<GameEventDto>>
		{
			private readonly IRepository<GameEvent> _gameEventsRepository;
			private readonly IMapper _mapper;

			public Handler(IRepository<GameEvent> gameEventsRepository, IMapper mapper)
			{
				_gameEventsRepository = gameEventsRepository;
				_mapper = mapper;
			}

			public async Task<PagingResponse<GameEventDto>> Handle(Query request, CancellationToken cancellationToken)
			{
				var spec = new GetGameEventsSpecification(request.Filter);

				var result = await _gameEventsRepository.ListAsync(spec);
				var totalCount = request.Filter.IsPagingEnabled
					? await _gameEventsRepository.CountAsync(spec)
					: result.Count;

				return new PagingResponse<GameEventDto>(request.Filter, _mapper.Map<List<GameEventDto>>(result), totalCount);
			}
		}
	}
}
