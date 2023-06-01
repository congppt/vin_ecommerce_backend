﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomService.Service;
using VinEcomViewModel.Base;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService shipperService;
        public ShipperController(IShipperService shipperService)
        {
            this.shipperService = shipperService;
        }
        [HttpPost("authorize")]
        public async Task<IActionResult> AuthorizeAsync([FromBody] SignInViewModel vm)
        {
            var result = await shipperService.AuthorizeAsync(vm);
            if (result is null) return Unauthorized(VinEcomService.Resources.VinEcom.VINECOM_USER_AUTHORIZE_FAILED);
            return Ok(result);
        }
    }
}
