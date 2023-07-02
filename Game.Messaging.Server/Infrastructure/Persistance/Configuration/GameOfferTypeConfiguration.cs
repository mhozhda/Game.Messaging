using Game.Messaging.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Messaging.Server.Infrastructure.Persistance.Configuration
{
	public class GameOfferTypeConfiguration : IEntityTypeConfiguration<GameOffer>
	{
		public void Configure(EntityTypeBuilder<GameOffer> builder)
		{
			builder.HasKey(x => x.Id);
		}
	}
}
