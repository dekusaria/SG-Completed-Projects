using GuildCars.Data.Factory;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class InventoryAPIController : ApiController
    {
        [Route("api/inventory/search/{vehicleTypeId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(int vehicleTypeId, string searchTerm, decimal? minPrice, decimal? maxPrice, int? minYear, int? maxYear)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            try
            {
                var parameters = new VehicleSearchParameters()
                {
                    VehicleTypeId = vehicleTypeId,
                    SearchTerm = searchTerm,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear
                };

                var result = repo.SearchVehicles(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/inventory/models/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetModelsByMakeId(int id)
        {
            var repo = ModelRepositoryFactory.GetRepository();

            try
            {
                var result = repo.GetModelsByMakeId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/inventory/checkvins/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult CheckVin(int id, string vin)
        {
            var repo = VehicleRepositoryFactory.GetRepository().GetAll().ToList();

            try
            {
                List<string> vins;

                if (id > 0)
                {
                    repo.RemoveAll(m => m.VehicleId == id);
                }

                vins = repo.Select(m => m.Vin).ToList();

                return vins.Contains(vin) ? Ok(false) : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/reports/sales/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchSalesReports(string userEmail, string minDate, string maxDate)
        {
            try
            {
                var repo = ReportsRepositoryFactory.GetRepository();

                var parameters = new SalesSearchParameters();

                parameters.UserEmail = userEmail;

                if (!string.IsNullOrEmpty(minDate))
                {
                    DateTime date = new DateTime();

                    if (DateTime.TryParse(minDate, out date))
                    {
                        parameters.MinDate = date;
                    }
                    else
                    {
                        return BadRequest("An error occured while parsing Min Date");
                    }
                }

                if (!string.IsNullOrEmpty(maxDate))
                {
                    DateTime date = new DateTime();

                    if (DateTime.TryParse(maxDate, out date))
                    {
                        parameters.MaxDate = date;
                    }
                    else
                    {
                        return BadRequest("An error occured while parsing Max Date");
                    }
                }

                var result = repo.SearchSalesReports(parameters);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
