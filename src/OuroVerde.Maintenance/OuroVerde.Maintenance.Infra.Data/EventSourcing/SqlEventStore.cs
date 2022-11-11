using Newtonsoft.Json;
using OuroVerde.Maintenance.Domain.Core.Events;
using OuroVerde.Maintenance.Domain.Core.Identity.User;
using OuroVerde.Maintenance.Domain.Core.Messaging;
using OuroVerde.Maintenance.Infra.Data.Repository.EventSourcing;

namespace OuroVerde.Maintenance.Infra.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IAspNetUser _user;

        public SqlEventStore(IEventStoreRepository eventStoreRepository, IAspNetUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public void Save<T>(T theEvent) where T : Event
        {
            // Using Newtonsoft.Json because System.Text.Json
            // is a sad joke to be considered "Done"

            // The System.Text don't know how serialize a
            // object with inherited properties, I said is sad...
            // Yes! I tried: options = new JsonSerializerOptions { WriteIndented = true };

            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                _user.Name ?? _user.GetUserEmail());

            _eventStoreRepository.Store(storedEvent);
        }
    }
}
