using Game.Messaging.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Messaging.Server.Infrastructure.Persistance.Configuration
{
	public class GameEventTypeConfiguration : IEntityTypeConfiguration<GameEvent>
	{
		public void Configure(EntityTypeBuilder<GameEvent> builder)
		{
			builder.HasKey(e => e.Id);
		}
	}
}
