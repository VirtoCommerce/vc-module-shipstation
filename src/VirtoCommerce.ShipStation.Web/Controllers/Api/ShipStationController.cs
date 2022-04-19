using VirtoCommerce.ShipStation.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace VirtoCommerce.ShipStation.Web.Controllers.Api
{
    [Route("api/ShipStation")]
    public class ShipStationController : Controller
    {
        // GET: api/VirtoCommerce.ShipStation
        /// <summary>
        /// Get message
        /// </summary>
        /// <remarks>Return "Hello world!" message</remarks>
        [HttpGet]
        [Route("")]
        [Authorize(ModuleConstants.Security.Permissions.Read)]
        public ActionResult<string> Get()
        {
            return Ok(new { result = "Hello world!" });
        }
    }
}