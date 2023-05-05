using ClassLibTeam02.Business;
using ClassLibTeam02.Business.Entities;
using ClassLibTeam02.Data;
using ClassLibTeam02.Data.Framework;
using ClassLibTeam02.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTeam02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        /// <summary>
        /// WebAPI-01
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetHouses()
        {
            try
            {
                return Ok(Houses.GetHouses());
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// WebAPI-02
        /// </summary>
        /// <param name="house"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostHouse(House house)
        {
            InsertResult insert = new();
            try
            {
                if (Houses.HasCity(house.HouseAdress.AreaCode))
                    insert = Houses.AddHouse(house);
                else
                    insert = Houses.AddHouseAndCity(house);
                return Ok(insert);
            }
            catch
            {
                return BadRequest(insert);
            }
        }
        
    }
}
