using Microsoft.AspNetCore.Mvc;

namespace OuroVerde.Maintenance.Application.Interface;

public interface IItensIntegrationService
{
    Task<ActionResult> sendItensQueue();
    Task<ActionResult> sendItensActived();
}
