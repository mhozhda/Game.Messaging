using Game.Messaging.Server.Entities;
using Game.Messaging.Server.Infrastructure.Persistance.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Game.Messaging.Server.Infrastructure.Persistance
{
	public class GameEventsDbContext : DbContext
	{
		public GameEventsDbContext(DbContextOptions<GameEventsDbContext> options) : base(options)
		{
		}

		public DbSet<GameEvent> GameEvents { get; set; }
		public DbSet<GameOffer> GameOffers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new GameEventTypeConfiguration());
			modelBuilder.ApplyConfiguration(new GameOfferTypeConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
