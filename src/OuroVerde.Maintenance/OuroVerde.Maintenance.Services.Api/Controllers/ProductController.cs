﻿using Microsoft.AspNetCore.Mvc;
using OuroVerde.Maintenance.Application.Interface;
using OuroVerde.Maintenance.Domain.Model;
using System.Net;
using static OuroVerde.Maintenance.Services.Api.Filters.HttpGlobalExceptionFilter;

namespace OuroVerde.Maintenance.Services.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ApiController
    {
        protected readonly IItensIntegrationService _service;

        public ProductController(IItensIntegrationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Envio de Itens Parados
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(JsonError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(JsonError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ProductSalesForceViewModel), (int)HttpStatusCode.OK)]
        [HttpPost("sendItensActived")]
        public async Task<ActionResult> sendItensActived()
        {
            return await _service.sendItensActived();
        }

        /// <summary>
        /// Envio de Itens de Taxa
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(JsonError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(JsonError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ProductSalesForceViewModel), (int)HttpStatusCode.OK)]
        [HttpPost("sendTaxItensQueue")]
        public async Task<ActionResult> sendTaxItensQueue()
        {
            return await _service.sendTaxItensQueue();
        }

    }
}