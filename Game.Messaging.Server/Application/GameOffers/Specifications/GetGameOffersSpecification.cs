﻿using Ardalis.Specification;
using Game.Messaging.Server.Application.GameOffers.Specifications.Filters;
using Game.Messaging.Server.Entities;

namespace Game.Messaging.Server.Application.GameOffers.Specifications
{
	public class GetGameOffersSpecification : Specification<GameOffer>
	{
		public GetGameOffersSpecification(GameOffersFilter filter)
		{
			if (filter.IsPagingEnabled)
			{
				Query.OrderBy(x => x.Id);
				Query.Skip(filter.PageSize * filter.Page - 1).Take(filter.Page);
			}

			if (!string.IsNullOrEmpty(filter.Name))
			{
				Query.Where(x => x.Name == filter.Name);
			}

			if (filter.StartsBefore.HasValue)
			{
				Query.Where(x => x.StartsAt <= filter.StartsBefore.Value);
			}

			if (filter.StartsAfter.HasValue)
			{
				Query.Where(x => x.StartsAt > filter.StartsAfter.Value);
			}

			if (filter.ExpiresBefore.HasValue)
			{
				Query.Where(x => x.ExpiresAt <= filter.ExpiresBefore.Value);
			}

			if (filter.ExpiresAfter.HasValue)
			{
				Query.Where(x => x.ExpiresAt > filter.ExpiresAfter.Value);
			}

			if (filter.OfferType.HasValue)
			{
				Query.Where(x => x.OfferType == filter.OfferType);
			}
		}
	}
}
