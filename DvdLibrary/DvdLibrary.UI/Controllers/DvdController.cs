using DvdLibrary.Data.Factory;
using DvdLibrary.Data.Interfaces;
using DvdLibrary.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DvdLibrary.UI.Controllers
{
    public class DvdController : ApiController
    {
        private IDvdRepository _repo;

        public DvdController()
        {
            _repo = DvdRepositoryFactory.GetRepository();
        }

        [Route("dvd")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddDvd(DvdItem model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var repo = DvdRepositoryFactory.GetRepository();

            try
            {
                _repo.Insert(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdById(int id)
        {
            //var repo = DvdRepositoryFactory.GetRepository();

            try
            {
                var result = _repo.GetDvdById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult UpdateDvd(DvdItem model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var repo = DvdRepositoryFactory.GetRepository();

            try
            {
                _repo.Update(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteDvd(int id)
        {
            //var repo = DvdRepositoryFactory.GetRepository();

            try
            {
                _repo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}