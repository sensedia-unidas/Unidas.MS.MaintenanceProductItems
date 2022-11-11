using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OuroVerde.Maintenance.Application.Interface;
using OuroVerde.Maintenance.Domain.Adapters.Repository;
using OuroVerde.Maintenance.Domain.Core.Mediator;
using OuroVerde.Maintenance.Domain.Model;

namespace OuroVerde.Maintenance.Application.AppServices
{
    public class ItensIntegrationService : IItensIntegrationService
    {
        private readonly IMapper _mapper;
        private readonly IItensIntegrationRepository _repository;
        private readonly IMediatorHandler _mediator;

        public ItensIntegrationService(IMapper mapper,
                                       IItensIntegrationRepository repository,
                                       IMediatorHandler mediator)
        {
            _mapper = mapper;
            _repository = repository;
            _mediator = mediator;
        }

        public Task<ActionResult> checkHierarchyChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult> sendItensActived()
        {
            return null;
        }

        public Task<ActionResult> sendItensQueue()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> sendOperationalServicesQueue()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> sendTaxItensQueue()
        {
            throw new NotImplementedException();
        }
    }
}
