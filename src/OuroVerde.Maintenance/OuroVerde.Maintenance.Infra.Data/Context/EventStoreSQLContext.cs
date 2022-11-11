﻿using Microsoft.EntityFrameworkCore;
using OuroVerde.Maintenance.Domain.Core.Events;
using OuroVerde.Maintenance.Infra.Data.Mappings;

namespace OuroVerde.Maintenance.Infra.Data.Context
{
    public class EventStoreSqlContext : DbContext
    {
        public EventStoreSqlContext(DbContextOptions<EventStoreSqlContext> options) : base(options) { }

        public DbSet<StoredEvent> StoredEvent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
