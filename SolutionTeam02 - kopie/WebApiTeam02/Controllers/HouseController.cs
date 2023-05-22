using ClassLibTeam02.Business;
using ClassLibTeam02.Business.Entities;
using ClassLibTeam02.Data;
using ClassLibTeam02.Data.Framework;
using ClassLibTeam02.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApiTeam02.Models;

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
                return Ok(JsonConvert.SerializeObject(HouseRepository.GetHouses()));
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
                if (HouseRepository.AreaCodeExists(house.HouseAdress.AreaCode))
                    insert = HouseRepository.AddHouse(house);
                else
                {
                    insert = HouseRepository.AddHouseAndCity(house);
                }
                return Ok(insert);
            }
            catch
            {
                return BadRequest(insert);
            }
        }
        
    }
}
