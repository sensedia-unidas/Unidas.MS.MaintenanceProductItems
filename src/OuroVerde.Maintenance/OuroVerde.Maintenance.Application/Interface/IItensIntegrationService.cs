using Microsoft.AspNetCore.Mvc;

namespace OuroVerde.Maintenance.Application.Interface;

public interface IItensIntegrationService
{
    Task<ActionResult> sendOperationalServicesQueue();
    Task<ActionResult> sendItensQueue();
    Task<ActionResult> sendItensActived();
    Task<ActionResult> sendTaxItensQueue();

    Task<ActionResult> checkHierarchyChanges();
}
