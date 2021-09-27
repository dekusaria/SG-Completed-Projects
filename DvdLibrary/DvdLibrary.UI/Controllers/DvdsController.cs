using DvdLibrary.Data.Factory;
using DvdLibrary.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DvdLibrary.UI.Controllers
{
    public class DvdsController : ApiController
    {
        private IDvdRepository _repo;

        public DvdsController()
        {
            _repo = DvdRepositoryFactory.GetRepository();
        }

        [Route("dvds")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            //var repo = DvdRepositoryFactory.GetRepository();

            try
            {
                var result = _repo.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dvds/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdsByTitle(string title)
        {
            //var repo = DvdRepositoryFactory.GetRepository();

            try
            {
                var result = _repo.SearchByTitle(title);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dvds/year/{releaseYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdsByReleaseYear(int releaseYear)
        {
            //var repo = DvdRepositoryFactory.GetRepository();

            try
            {
                var result = _repo.SearchByReleaseYear(releaseYear);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dvds/director/{directorName}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdsByDirector(string directorName)
        {
            //var repo = DvdRepositoryFactory.GetRepository();

            try
            {
                var result = _repo.SearchByDirector(directorName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dvds/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdsByRating(string rating)
        {
            //var repo = DvdRepositoryFactory.GetRepository();

            try
            {
                var result = _repo.SearchByRating(rating);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}