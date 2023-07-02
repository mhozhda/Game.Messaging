using AutoMapper;
using Game.Messaging.Server.Application.Common.Paging;
using Game.Messaging.Server.Application.GameOffers.Dtos;
using Game.Messaging.Server.Application.GameOffers.Specifications;
using Game.Messaging.Server.Application.GameOffers.Specifications.Filters;
using Game.Messaging.Server.Entities;
using Game.Messaging.Server.Infrastructure.Persistance;
using MediatR;

namespace Game.Messaging.Server.Application.GameOffers.Queries
{
    public static class GetGameOffers
    {
        public record Query(GameOffersFilter Filter) : IRequest<PagingResponse<GameOfferDto>>;

        class Handler : IRequestHandler<Query, PagingResponse<GameOfferDto>>
        {
            private readonly IRepository<GameOffer> _gameOffersRepository;
            private readonly IMapper _mapper;

            public Handler(IRepository<GameOffer> gameOffersRepository, IMapper mapper)
            {
                _gameOffersRepository = gameOffersRepository;
                _mapper = mapper;
            }

            public async Task<PagingResponse<GameOfferDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var spec = new GetGameOffersSpecification(request.Filter);

                var result = await _gameOffersRepository.ListAsync(spec);
                var totalCount = request.Filter.IsPagingEnabled
                    ? await _gameOffersRepository.CountAsync(spec)
                    : result.Count;

                return new PagingResponse<GameOfferDto>(request.Filter, _mapper.Map<List<GameOfferDto>>(result), totalCount);
            }
        }
    }
}
